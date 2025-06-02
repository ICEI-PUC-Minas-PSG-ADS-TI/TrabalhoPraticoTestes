using System.Diagnostics.SymbolStore;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Trabalho_POO;

namespace trabalhoPOOList
{
    public class Program
    {

        public static void Main(string[] args)
        {
            RepositorioMongoDB repo = new RepositorioMongoDB();
            bool continuar = true;
            int opcao = 0;

            while (continuar)
            {
                Console.WriteLine("************** Bem-Vindo a Festa & Cia **************");

                Console.WriteLine(" Escolha uma opção\n");
                Console.WriteLine(" OPÇÃO 1: Listar datas agendadas");
                Console.WriteLine(" OPÇÃO 2: Agendar evento");
                Console.WriteLine(" Opção 0 : Encerrar Programa");

                opcao = OpcaoMenuInicial();
                if (!IsTestingEnvironment) // Add this check here too
                {
                    Console.Clear();
                }

                // Executa a ação correspondente à opção escolhida
                switch (opcao)
                {
                    case 1:
                        ExibirReservas(repo);
                        break;
                    case 2:
                        MenuAgendamento();
                        break;
                    case 0:
                        Console.WriteLine("Fim do programa");
                        continuar = false; // This will cause the while loop to terminate
                        break;
                }
            }

        }

        public static bool IsTestingEnvironment { get; set; } = false;
        // Menu para agendamento de eventos
        public static void MenuAgendamento()
        {
            GerenciadorFesta reserva;
            RepositorioMongoDB repositorio = new RepositorioMongoDB();

            int qtdConvidados;
            List<int> qtdBebidas;

            int opcao = 0;

            Console.WriteLine(" Escolha o tipo de reserva desejado \n");
            Console.WriteLine(" Opção 1 : Casamento standard");
            Console.WriteLine(" Opção 2 : Casamento luxo");
            Console.WriteLine(" Opção 3 : Casamento premier");
            Console.WriteLine(" Opção 4 : Festa de Empresa standard");
            Console.WriteLine(" Opção 5 : Festa de Empresa luxo");
            Console.WriteLine(" Opção 6 : Festa de Empresa premier");
            Console.WriteLine(" Opção 7 : Formatura standard");
            Console.WriteLine(" Opção 8 : Formatura luxo");
            Console.WriteLine(" Opção 9 : Formatura Premier");
            Console.WriteLine(" Opção 10 : Aniversário");
            Console.WriteLine(" Opção 11 : Evento livre");
            Console.WriteLine(" Opção 0 : Encerrar Programa");
            opcao = OpcaoTipoCasamento();
            if (!IsTestingEnvironment) // THIS IS THE CHANGE for line 70
            {
                Console.Clear();
            }

            switch (opcao)
            {
                case 0:
                    Console.WriteLine(" Fim do programa");

                    break;
                case 1:
                    (qtdConvidados, qtdBebidas) = MenuStandard();
                    reserva = new GerenciadorFesta(qtdConvidados, "CasamentoStandard", qtdBebidas);
                    ExibirDadosDoEvento(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumo()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados())); ;
                    break;
                case 2:
                    (qtdConvidados, qtdBebidas) = MenuLuxo();
                    reserva = new GerenciadorFesta(qtdConvidados, "CasamentoLuxo", qtdBebidas);
                    ExibirDadosDoEvento(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumo()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados())); ;
                    break;
                case 3:
                    (qtdConvidados, qtdBebidas) = MenuPremier();
                    reserva = new GerenciadorFesta(qtdConvidados, "CasamentoPremier", qtdBebidas);
                    ExibirDadosDoEvento(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumo()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados())); ;
                    break;
                case 4:
                    (qtdConvidados, qtdBebidas) = MenuStandard();
                    reserva = new GerenciadorFesta(qtdConvidados, "FestaEmpresaStandard", qtdBebidas);
                    ExibirDadosDoEvento(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumo()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados()));
                    break;
                case 5:
                    (qtdConvidados, qtdBebidas) = MenuLuxo();
                    reserva = new GerenciadorFesta(qtdConvidados, "FestaEmpresaLuxo", qtdBebidas);
                    ExibirDadosDoEvento(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumo()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados())); ;
                    break;
                case 6:
                    (qtdConvidados, qtdBebidas) = MenuPremier();
                    reserva = new GerenciadorFesta(qtdConvidados, "FestaEmpresaPremier", qtdBebidas);
                    ExibirDadosDoEvento(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumo()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados())); ;
                    break;
                case 7:
                    (qtdConvidados, qtdBebidas) = MenuStandard();
                    reserva = new GerenciadorFesta(qtdConvidados, "FormaturaStandard", qtdBebidas);
                    ExibirDadosDoEvento(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumo()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados())); ;
                    break;
                case 8:
                    (qtdConvidados, qtdBebidas) = MenuLuxo();
                    reserva = new GerenciadorFesta(qtdConvidados, "FormaturaLuxo", qtdBebidas);
                    ExibirDadosDoEvento(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumo()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados())); ;
                    break;
                case 9:
                    (qtdConvidados, qtdBebidas) = MenuPremier();
                    reserva = new GerenciadorFesta(qtdConvidados, "FormaturaPremier", qtdBebidas);
                    ExibirDadosDoEvento(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumo()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados())); ;
                    break;
                case 10:
                    (qtdConvidados, qtdBebidas) = MenuStandard();
                    reserva = new GerenciadorFesta(qtdConvidados, "FestaAniversario", qtdBebidas);
                    ExibirDadosDoEvento(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumo()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados())); ;
                    break;
                case 11:
                    qtdConvidados = MenuEventoLivre();
                    reserva = new GerenciadorFesta(qtdConvidados);
                    ExibirDadosDoEventoLivre(reserva);
                    repositorio.GuardaDadosResumo(repositorio.ConverterResumoFesta(reserva.GerarResumoEventoLivre()));
                    repositorio.GuardaDatasReservadas(repositorio.ConverterDadosReservadaEspaco(reserva.GerarDados())); ;
                    break;
            }
        }
        // Menu para eventos standard
        public static (int, List<int>) MenuStandard()
        {
            List<int> qtdBebidas = new List<int>();
            int qtdParticipantes = 0;

            Console.Write(" Digite a quantidade de participantes : ");
            qtdParticipantes = QuantidadeParticipantes();

            Console.WriteLine(" Digite a quantidade de bebida desejada");
            Console.Write(" Água sem gás (1,5L) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Suco (1L) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Refrigerante (2L) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Cerveja Comum (600ml) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Espumante nacional (750ml) : ");
            qtdBebidas.Add(QuantidadeBebida());
            if (!IsTestingEnvironment) // <<< ADD THIS CHECK
            {
                Console.Clear();
            }

            return (qtdParticipantes, qtdBebidas);

        }
        // Menu para eventos Luxo
        public static (int, List<int>) MenuLuxo()
        {
            List<int> qtdBebidas = new List<int>();
            int qtdParticipantes = 0;

            Console.Write(" Digite a quantidade de participantes : ");
            qtdParticipantes = QuantidadeParticipantes();

            Console.WriteLine(" Digite a quantidade de bebida desejada");
            Console.Write(" Água sem gás (1,5L) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Suco (1L) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Refrigerante (2L) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Cerveja Comum (600ml) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Cerveja Artesanal (600ml) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Espumante nacional (750ml) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Espumante importado (750ml) : ");
            qtdBebidas.Add(QuantidadeBebida());
            if (!IsTestingEnvironment) // <<< ADD THIS CHECK
            {
                Console.Clear();
            }

            return (qtdParticipantes, qtdBebidas);
        }
        // Menu para eventos Premier
        public static (int, List<int>) MenuPremier()
        {
            List<int> qtdBebidas = new List<int>();
            int qtdParticipantes = 0;

            Console.Write(" Digite a quantidade de participantes : ");
            qtdParticipantes = QuantidadeParticipantes();

            Console.WriteLine(" Digite a quantidade de bebida desejada");
            Console.Write(" Água sem gás (1,5L) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Suco (1L) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Refrigerante (2L) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Cerveja Comum (600ml) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Cerveja Artesanal (600ml) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Espumante nacional (750ml) : ");
            qtdBebidas.Add(QuantidadeBebida());
            Console.Write(" Espumante importado (750ml) : ");
            qtdBebidas.Add(QuantidadeBebida());
            if (!IsTestingEnvironment) // <<< ADD THIS CHECK
            {
                Console.Clear();
            }

            return (qtdParticipantes, qtdBebidas);
        }
        // Menu para eventos livres
        public static int MenuEventoLivre()
        {
            int qtdParticipantes;
            Console.Write(" Digite a quantidade de participantes : ");
            qtdParticipantes = QuantidadeParticipantes();

            return qtdParticipantes;
        }
        // Captura a quantidade de bebida ate que seja valida
        public static int QuantidadeBebida()
        {
            bool continuar = true;
            int quantidade = 0;
            while (continuar)
            {
                if (!int.TryParse(Console.ReadLine(), out quantidade))
                {
                    Console.WriteLine($"Quantidade {quantidade} inválida, informe uma quantidade válida.");
                }
                else
                {
                    continuar = false;
                }
            }

            return quantidade;
        }
        // Captura a quantidade de participantes ate que seja valida
        public static int QuantidadeParticipantes()
        {
            bool continuar = true;
            int quantidade = 0;
            while (continuar)
            {
                if (!int.TryParse(Console.ReadLine(), out quantidade))
                {
                    Console.WriteLine($"Quantidade {quantidade} inválida, informe uma quantidade válida.");
                }
                else
                {
                    continuar = false;
                }
            }

            return quantidade;
        }
        // Captura a opção do tipo de casamento ate que seja valida
        public static int OpcaoTipoCasamento()
        {
            bool continuar = true;
            int opcao = 0;
            while (continuar)
            {
                if (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 0 && opcao > 3)
                {
                    Console.WriteLine($"Opção {opcao} inválida");
                    Console.WriteLine("Tente Novamente, ou digite 0 para sair");
                }
                else
                {
                    continuar = false;
                }
            }

            return opcao;
        }
        // Captura a opção do menu inicial ate que seja valida
        public static int OpcaoMenuInicial()
        {
            bool continuar = true;
            int opcao = 0;
            while (continuar)
            {
                if (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 0 && opcao > 2)
                {
                    Console.WriteLine($"Opção {opcao} inválida");
                    Console.WriteLine("Tente Novamente, ou digite 0 para sair");
                }
                else
                {
                    continuar = false;
                }
            }

            return opcao;
        }
        // Exibir os dados do evento escolhido
        public static void ExibirDadosDoEvento(GerenciadorFesta festa)
        {
            try
            {
                Console.WriteLine("*************** Confira os dados do evento ***************\n");

                //Tipo da festa
                Console.WriteLine($"Tipo de festa: {festa.GerarTipoFesta.TipoFesta}");
                ResumoUtensilios(festa);
                ResumoComidas(festa);
                ResumoBebidas(festa);
                ResumoEspaco(festa);

                //Data da reserva
                Console.WriteLine($"\n Data da reserva: {festa.Reservas.DataReservada: dd/MM/yyyy}");

                // Valor total do evento
                Console.WriteLine($"\n VALOR TOTAL DA FESTA: {festa.ValorTotal.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir os dados do evento: {ex.Message}");
            }
        }
        // Exibir os dados do evento livre
        public static void ExibirDadosDoEventoLivre(GerenciadorFesta festa)
        {
            try
            {
                Console.WriteLine("*************** Confira os dados do evento ***************\n");

                //Tipo da festa
                Console.WriteLine($"Tipo de festa: {festa.GerarTipoFesta.TipoFesta}");

                //Data da reserva
                Console.WriteLine($"\n Data da reserva: {festa.Reservas.DataReservada: dd/MM/yyyy}");

                // Valor total do evento
                Console.WriteLine($"\n VALOR TOTAL DA FESTA: {festa.ValorTotal.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir os dados do evento livre: {ex.Message}");
            }
        }
        // resumo dos utensilios usados
        public static void ResumoUtensilios(GerenciadorFesta festa)
        {
            try
            {
                if (festa.GerarTipoFesta.DadosUtensilios != null)
                {
                    // DadosItensBase (utensílios predefinidos para o tipo de festa)
                    Console.WriteLine(" +++++ Utensílios disponibilizados +++++\n");

                    festa.GerarTipoFesta.DadosUtensilios
                        .ToList()
                        .ForEach(item => Console.WriteLine($" {item.Key}, Valor: {item.Value.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}"));

                    Console.WriteLine($" Valor total dos utensílios {festa.ValorTotalUtensilios.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir o resumo dos utensílios: {ex.Message}");
            }
        }
        // resumo das comidas usadas
        public static void ResumoComidas(GerenciadorFesta festa)
        {
            try
            {
                // Itens_Comida (itens predefinidos para o tipo de festa)
                Console.WriteLine("\n +++++ Comidas da festa +++++\n");

                festa.GerarTipoFesta.Comida.Itens_Comida.ToList()
                    .ForEach(item => Console.WriteLine($" {item.Key}, Valor: {item.Value.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}"));

                Console.WriteLine($" Valor total das comidas {festa.ValorTotalComidas.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir o resumo das comidas: {ex.Message}");
            }
        }
        // resumo das bebidas usadas
        public static void ResumoBebidas(GerenciadorFesta festa)
        {
            try
            {
                // Lista_Bebidas (itens disponíveis para a festa, escolhidos pelos noivos)
                Console.WriteLine("\n +++++ Bebidas escolhidas +++++\n");

                festa.GerarTipoFesta.Bebida.Lista_Bebidas.Where((bebida, indice) => festa.GerarTipoFesta.Bebida.Qtd_Bebida[indice] > 0).ToList()
                    .ForEach(item => Console.WriteLine($" {item.Key}, Valor: {item.Value.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}, " +
                    $"Quantidade: {festa.GerarTipoFesta.Bebida.Qtd_Bebida[festa.GerarTipoFesta.Bebida.Lista_Bebidas.Keys.ToList().IndexOf(item.Key)]}"));

                Console.WriteLine($" Valor total de bebidas: {festa.ValorTotalBebidas.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir o resumo das bebidas: {ex.Message}");
            }
        }
        // resumo dos espacos usados
        public static void ResumoEspaco(GerenciadorFesta festa)
        {
            try
            {
                // Dados do espaço reservado
                Console.WriteLine("\n +++++ Espaço reservado +++++\n");
                Console.WriteLine($" Capacidade do espaço: {festa.Reservas.Espaco.CapacidadeDoEspaco.ToString()}");
                Console.WriteLine($" Valor do espaço: {festa.Reservas.Espaco.Preco.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir o resumo do espaço: {ex.Message}");
            }
        }
        //Exibir reservas efetuadas anteriomente
        public static void ExibirReservas(RepositorioMongoDB repo)
        {
            try
            {
                var resumos = repo.RecuperarResumosFesta();
                if (resumos.Count == 0)
                {
                    Console.WriteLine("Nenhuma reserva encontrada. \n");
                }
                else
                {
                    resumos.ForEach(resumo =>
                    {
                        Console.WriteLine("Data da Reserva: " + ((DateTime)resumo["Data da reserva"]).ToString("dd/MM/yyyy"));
                        Console.WriteLine("Tipo de festa: " + resumo["Tipo de festa"] + "\n");
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir as reservas: {ex.Message}");
            }
        }




    }
}