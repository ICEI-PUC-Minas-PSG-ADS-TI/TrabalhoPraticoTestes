using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class CalculadoraEventos
    {
        private double ValorTotal { get; }

        public double CalcularValorTotal(Evento festa, int quantidadeParticipantes, Reserva espacoReserva)
        {
            try
            {
                double valorTotal = 0;
                valorTotal += CalcularBebidas(festa) + CalcularComidas(festa, quantidadeParticipantes) + CalcularUtensilios(festa, espacoReserva) + espacoReserva.Espaco.Preco;
                return valorTotal;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Erro: Um dos argumentos fornecidos é nulo. Detalhes: {ex.Message}");
                return 0;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro: Operação inválida ao calcular o valor total. Detalhes: {ex.Message}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: Ocorreu um erro ao calcular o valor total. Detalhes: {ex.Message}");
                return 0;
            }
        }
        public double CalcularBebidas(Evento festa)
        {
            try
            {
                double valorTotal = 0;
                valorTotal = festa.Bebida.Lista_Bebidas.Values
                    .Select((bebida, indice) => bebida * festa.Bebida.Qtd_Bebida[indice])
                    .Where((bebida, indice) => festa.Bebida.Qtd_Bebida[indice] > 0)
                    .Sum();

                return valorTotal;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Erro: Um dos argumentos fornecidos é nulo. Detalhes: {ex.Message}");
                return 0;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Erro: Índice fora dos limites ao calcular bebidas. Detalhes: {ex.Message}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: Ocorreu um erro ao calcular bebidas. Detalhes: {ex.Message}");
                return 0;
            }
        }
        public double CalcularComidas(Evento festa, int quantidadeParticipantes)
        {
            try
            {
                double valorTotal = 0;
                valorTotal = festa.Comida.Itens_Comida.Values
                    .Select((comida, indice) => comida * quantidadeParticipantes)
                    .Sum();

                return valorTotal;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Erro: Um dos argumentos fornecidos é nulo. Detalhes: {ex.Message}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: Ocorreu um erro ao calcular comidas. Detalhes: {ex.Message}");
                return 0;
            }
        }
        public double CalcularUtensilios(Evento festa, Reserva espacoReserva)
        {
            try
            {
                double valorTotal = 0;
                valorTotal = festa.DadosUtensilios.Values
                    .Select((utensilio, indice) => utensilio * espacoReserva.Espaco.CapacidadeDoEspaco)
                    .Sum();

                return valorTotal;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Erro: Um dos argumentos fornecidos é nulo. Detalhes: {ex.Message}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: Ocorreu um erro ao calcular utensílios. Detalhes: {ex.Message}");
                return 0;
            }
        }
    }
}
