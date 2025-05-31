using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class ConexaoMongoDb // Conecta ao banco de dados
    {
        private readonly IMongoDatabase database;
        private readonly string StringConexao = "mongodb://localhost:27017/";
        private readonly string BancoDeDados = "Trabalho_POO";

        public ConexaoMongoDb()
        {
            try
            {
                var client = new MongoClient(this.StringConexao);
                this.database = client.GetDatabase(this.BancoDeDados);
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception($"Erro de conexão com o banco de dados: {ex.Message}");
            }
        }

        // Integra a coleção que guarda todos os dados do evento
        public IMongoCollection<BsonDocument> GetColecaoDadosDosEventos()
        {
            try
            {
                return this.database.GetCollection<BsonDocument>("Dados_Dos_Eventos");
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception($"Erro de conexão com o banco de dados: {ex.Message}");
            }
        }

        // Integra a coleção que guarda datas reservadas e tipo do evento
        public IMongoCollection<BsonDocument> GetColecaoReservasEfetuadas()
        {
            try
            {
                return this.database.GetCollection<BsonDocument>("Reservas_Efetuadas");
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception($"Erro de conexão com o banco de dados: {ex.Message}");
            }
        }
    }


}
