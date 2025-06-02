using DnsClient;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using trabalhoPOO;
using trabalhoPOOList;

namespace Trabalho_POO
{
    public class GerenciadorFesta
    {
        public Evento GerarTipoFesta { get; protected set; }
        public double ValorTotalComidas { get; private set; }
        public double ValorTotalBebidas { get; private set; }
        public double ValorTotalUtensilios { get; private set; }
        public double ValorTotal { get; private set; }
        public int QuantidadeParticipantes { get; private set; }

        public Reserva Reservas;
        private CalculadoraEventos Calculadora;

        // Construtor principal com injeção de dependência para Reserva
        public GerenciadorFesta(int quantidadeDeParticipantes, string tipoDeFesta, List<int> quantidadesBebidas, Reserva reserva = null)
        {
            this.Reservas = reserva ?? InstanciarReserva(quantidadeDeParticipantes);
            this.Reservas.Reservar();
            this.QuantidadeParticipantes = quantidadeDeParticipantes;
            this.GerarTipoFesta = InstanciarEvento(tipoDeFesta, quantidadesBebidas);
            this.Calculadora = new CalculadoraEventos();
            this.ValorTotalComidas = this.Calculadora.CalcularComidas(this.GerarTipoFesta, quantidadeDeParticipantes);
            this.ValorTotalBebidas = this.Calculadora.CalcularBebidas(this.GerarTipoFesta);
            this.ValorTotalUtensilios = this.Calculadora.CalcularUtensilios(this.GerarTipoFesta, this.Reservas);
            this.ValorTotal = this.Calculadora.CalcularValorTotal(this.GerarTipoFesta, quantidadeDeParticipantes, this.Reservas);
        }

        public GerenciadorFesta(int quantidadeDeParticipantes, Reserva reserva = null)
        {
            this.Reservas = reserva ?? InstanciarReserva(quantidadeDeParticipantes);
            this.Reservas.Reservar();
            this.GerarTipoFesta = new EventoLivre();
            this.ValorTotal = this.Reservas.Espaco.Preco;
        }

        // instancia o tipo de evento
        private Evento InstanciarEvento(string tipofesta, List<int> quantidadesBebidas)
        {
            try
            {
                switch (tipofesta)
                {
                    case "CasamentoStandard":
                        return new CasamentoStandard(quantidadesBebidas);
                    case "CasamentoLuxo":
                        return new CasamentoLuxo(quantidadesBebidas);
                    case "CasamentoPremier":
                        return new CasamentoPremier(quantidadesBebidas);
                    case "FestaEmpresaStandard":
                        return new FestaEmpresaStandard(quantidadesBebidas);
                    case "FestaEmpresaLuxo":
                        return new FestaEmpresaLuxo(quantidadesBebidas);
                    case "FestaEmpresaPremier":
                        return new FestaEmpresaPremier(quantidadesBebidas);
                    case "FormaturaStandard":
                        return new FormaturaStandard(quantidadesBebidas);
                    case "FormaturaLuxo":
                        return new FormaturaLuxo(quantidadesBebidas);
                    case "FormaturaPremier":
                        return new FormaturaPremier(quantidadesBebidas);
                    case "FestaAniversario":
                        return new FestaAniversario(quantidadesBebidas);
                    default:
                        throw new ArgumentException("Tipo de festa inválido.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: Ocorreu um erro ao instanciar o evento. Detalhes: {ex.Message}");
                throw; // Re-lança a exceção para que o chamador possa tratá-la
            }
        }


        // instancia o tipo de reserva (Espaco com capacidade adequada a quantidade de pessoas)
        private static Reserva InstanciarReserva(int quantidadeDeParticipantes)
        {
            try
            {
                if (quantidadeDeParticipantes > 0 && quantidadeDeParticipantes <= 50)
                {
                    return new ReservaCinquenta();
                }
                else if (quantidadeDeParticipantes > 50 && quantidadeDeParticipantes <= 100)
                {
                    return new ReservaCem();
                }
                else if (quantidadeDeParticipantes > 100 && quantidadeDeParticipantes <= 200)
                {
                    return new ReservaDuzentos();
                }
                else if (quantidadeDeParticipantes > 200 && quantidadeDeParticipantes <= 500)
                {
                    return new ReservaQuinhentos();
                }
                else
                {
                    throw new ArgumentException("Capacidade do espaço inválida.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: Ocorreu um erro ao instanciar a reserva. Detalhes: {ex.Message}");
                return null;
            }
        }

        // Gerar o resumo do evento
        public ResumoFesta GerarResumo()
        {
            var resumo = new ResumoFesta();

            // Tipo de festa
            resumo.TipoFesta = this.GerarTipoFesta.TipoFesta;

            // Resumo Utensilios
                this.GerarTipoFesta.DadosUtensilios
                    .ToList()
                    .ForEach(item => resumo.Utensilios[item.Key] = item.Value);
                resumo.ValorTotalUtensilios = this.ValorTotalUtensilios;
           

            // Resumo Comidas
                this.GerarTipoFesta.Comida.Itens_Comida
                    .ToList()
                    .ForEach(item => resumo.Comidas[item.Key] = item.Value);
                resumo.ValorTotalComidas = this.ValorTotalComidas;
  

            // Resumo Bebidas
                resumo.Bebidas.Add(this.GerarTipoFesta.Bebida);
                resumo.ValorTotalBebidas = this.ValorTotalBebidas;
            

            // Data do evento
            resumo.DataReservada = this.Reservas.DataReservada;

            // Resumo Espaço
            resumo.CapacidadeEspaco = this.Reservas.Espaco.CapacidadeDoEspaco;
            resumo.ValorEspaco = this.Reservas.Espaco.Preco;

            // preço total do evento
            resumo.ValorTotalFesta = Calculadora.CalcularValorTotal(this.GerarTipoFesta, this.QuantidadeParticipantes, this.Reservas);

            return resumo;
        }

        // Gera o resumo do evento livre
        public ResumoFesta GerarResumoEventoLivre()
        {
            var resumo = new ResumoFesta();

            // Tipo de festa
            resumo.TipoFesta = this.GerarTipoFesta.TipoFesta;

            // Data do evento
            resumo.DataReservada = this.Reservas.DataReservada;

            // Resumo Espaço
            resumo.CapacidadeEspaco = this.Reservas.Espaco.CapacidadeDoEspaco;
            resumo.ValorEspaco = this.Reservas.Espaco.Preco;

            resumo.ValorTotalFesta = this.Reservas.Espaco.Preco;
            return resumo;
        }

        // Gera os dados para guardar no bd e retornar a ultima data reservada.
        public DadosReserva GerarDados()
        {
            var dados = new DadosReserva();

            // Data do evento
            dados.DataReservada = this.Reservas.DataReservada;
            // Resumo Espaço
            dados.CapacidadeEspaco = this.Reservas.Espaco.CapacidadeDoEspaco;
            dados.QtdReservas = this.Reservas.QuantidadeDeReservas;

            return dados;
        }
        
    }
}
