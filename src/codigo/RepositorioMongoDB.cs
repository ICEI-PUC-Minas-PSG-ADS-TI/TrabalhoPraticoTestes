using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class RepositorioMongoDB // Classe para manipulação dos dados no BD
    {
        private readonly ConexaoMongoDb conexaoMongoDb;

        public RepositorioMongoDB()
        {
            this.conexaoMongoDb = new ConexaoMongoDb();
        }

        // Guarda os dados do evento na coleção de dados do evento
        public void GuardaDadosResumo(BsonDocument documento)
        {
            try
            {
                var colecao = this.conexaoMongoDb.GetColecaoDadosDosEventos();
                colecao.InsertOne(documento);
            }
            catch (Exception ex1)
            {
                throw new Exception($"Erro ao tentar guardar os dados, {ex1}");
            }
        }

        // Converte os dados do evento em Bson para armazenamento
        public BsonDocument ConverterResumoFesta(ResumoFesta resumo)
        {
            try
            {
                var documento = new BsonDocument
                {
                    { "Tipo de festa", resumo.TipoFesta },
                    { "Utensilios", new BsonDocument(resumo.Utensilios.Select(kv => new BsonElement(kv.Key, kv.Value))) },
                    { "ValorTotalUtensilios", resumo.ValorTotalUtensilios },
                    { "Comidas", new BsonDocument(resumo.Comidas.Select(kv => new BsonElement(kv.Key, kv.Value))) },
                    { "ValorTotalComidas", resumo.ValorTotalComidas },
                    { "Bebidas", new BsonArray(resumo.Bebidas.Select(b => new BsonDocument
                    {
                    { "Lista_Bebidas", new BsonDocument(b.Lista_Bebidas.Select((kv, indice) => new BsonElement(kv.Key + " (Quantidade: " + b.Qtd_Bebida[indice] + ")", kv.Value))) }
                    })) },
                    { "ValorTotalBebidas", resumo.ValorTotalBebidas },
                    { "Data da reserva", resumo.DataReservada },
                    { "CapacidadeEspaco", resumo.CapacidadeEspaco },
                    { "ValorEspaco", resumo.ValorEspaco },
                    { "Valor total do evento", resumo.ValorTotalFesta },
                };

                return documento;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao guardar o resumo da festa: {ex.Message}");
            }
        }

        // recupera o resumo de eventos agendados
        public List<BsonDocument> RecuperarResumosFesta()
        {
            try
            {
                var colecao = this.conexaoMongoDb.GetColecaoDadosDosEventos();
                var documentos = colecao.Find(new BsonDocument()).ToList();
                return documentos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao recuperar resumos da festa: {ex.Message}");
                return new List<BsonDocument>();
            }
        }

        // Guarda os as datas, espaço e tipo de festa na coleção Reservas Efetuadas
        public void GuardaDatasReservadas(BsonDocument documento)
        {
            try
            {
                var colecao = this.conexaoMongoDb.GetColecaoReservasEfetuadas();
                var filter = Builders<BsonDocument>.Filter.Eq("CapacidadeEspaco", documento["CapacidadeEspaco"]);
                var options = new ReplaceOptions { IsUpsert = true };
                colecao.ReplaceOne(filter, documento, options);
            }
            catch (Exception ex1)
            {
                throw new Exception($"Erro ao tentar guardar os dados, {ex1.Message}");
            }
        }

        // Converte os dados de agendamentos efetuados em Bson para armazenamento
        public BsonDocument ConverterDadosReservadaEspaco(DadosReserva reservas)
        {
            try
            {
                var documento = new BsonDocument
            {
                { "Data da reserva", reservas.DataReservada },
                { "CapacidadeEspaco", reservas.CapacidadeEspaco },
                { "QuantidadeReservas", reservas.QtdReservas }
            };

                return documento;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao guardar os dados da reserva: {ex.Message}");
            }
        }

        // as informações de reservas efetuadas (data e quantidade de reserva)
        public DadosReserva RecuperarDadosReserva(int capacidadeEspaco)
        {
            try
            {
                var colecao = this.conexaoMongoDb.GetColecaoReservasEfetuadas();
                var filter = Builders<BsonDocument>.Filter.Eq("CapacidadeEspaco", capacidadeEspaco);
                var documento = colecao.Find(filter).FirstOrDefault();

                if (documento != null)
                {
                    return new DadosReserva
                    {
                        DataReservada = documento["Data da reserva"].ToUniversalTime(),
                        QtdReservas = documento["QuantidadeReservas"].AsInt32
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao recuperar os dados da reserva: {ex.Message}");
            }
        }
    }
}
