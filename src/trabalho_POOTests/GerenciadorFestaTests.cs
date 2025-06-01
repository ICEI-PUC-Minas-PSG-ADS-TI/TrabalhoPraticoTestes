using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trabalho_POO.Tests
{
    public class ResumoFesta
    {
        public string TipoFesta { get; set; }
        public Dictionary<string, double> Utensilios { get; set; } = new Dictionary<string, double>();
        public double ValorTotalUtensilios { get; set; }
        public Dictionary<string, double> Comidas { get; set; } = new Dictionary<string, double>();
        public double ValorTotalComidas { get; set; }
        public List<LocalTestBebidaBase> Bebidas { get; set; } = new List<LocalTestBebidaBase>();
        public double ValorTotalBebidas { get; set; }
        public DateTime DataReservada { get; set; }
        public int CapacidadeEspaco { get; set; }
        public double ValorEspaco { get; set; }
        public double ValorTotalFesta { get; set; }
    }

    public class DadosReserva
    {
        public DateTime DataReservada { get; set; }
        public int CapacidadeEspaco { get; set; }
        public int QtdReservas { get; set; }
    }

    public class StubEspaco
    {
        public int CapacidadeDoEspaco { get; set; }
        public double Preco { get; set; }
        public StubEspaco(int capacidade, double preco) { CapacidadeDoEspaco = capacidade; Preco = preco; }
    }
    public abstract class StubReservaBase
    {
        public StubEspaco Espaco { get; protected set; }
        public static DateTime DataDaUltimaReserva { get; set; } = DateTime.Now.Date;
        public int QuantidadeDeReservas { get; protected set; } = 0;
        public DateTime DataReservada { get; protected set; }
        public abstract void Reservar();
        protected StubReservaBase() { }
        protected DateTime ObterProximaData(DateTime dataAtual) => dataAtual.AddDays(1);
        protected DateTime EncontrarDataDisponivel() => DataDaUltimaReserva;
    }
    public class StubReservaCinquenta : StubReservaBase
    { 
        public StubReservaCinquenta() { Espaco = new StubEspaco(50, 150.0); }
        public override void Reservar()
        {
            if (QuantidadeDeReservas > 0) DataReservada = ObterProximaData(DataDaUltimaReserva);
            else DataReservada = EncontrarDataDisponivel();
            QuantidadeDeReservas++; DataDaUltimaReserva = DataReservada;
        }
    }
    public class StubReservaCem : StubReservaBase
    { 
        public StubReservaCem() { Espaco = new StubEspaco(100, 250.0); }
        public override void Reservar()
        {
            if (QuantidadeDeReservas > 0 && QuantidadeDeReservas % 4 == 0) DataReservada = ObterProximaData(DataDaUltimaReserva);
            else if (QuantidadeDeReservas == 0) DataReservada = EncontrarDataDisponivel();
            else DataReservada = DataDaUltimaReserva;
            if (DataReservada != DataDaUltimaReserva || QuantidadeDeReservas == 0) DataDaUltimaReserva = DataReservada;
            QuantidadeDeReservas++;
        }
    }
    public class StubReservaDuzentos : StubReservaBase
    { 
        public StubReservaDuzentos() { Espaco = new StubEspaco(200, 400.0); }
        public override void Reservar()
        {
            if (QuantidadeDeReservas > 0 && QuantidadeDeReservas % 2 == 0) DataReservada = ObterProximaData(DataDaUltimaReserva);
            else if (QuantidadeDeReservas == 0) DataReservada = EncontrarDataDisponivel();
            else DataReservada = DataDaUltimaReserva;
            if (DataReservada != DataDaUltimaReserva || QuantidadeDeReservas == 0) DataDaUltimaReserva = DataReservada;
            QuantidadeDeReservas++;
        }
    }
    public class StubReservaQuinhentos : StubReservaBase
    { 
        public StubReservaQuinhentos() { Espaco = new StubEspaco(500, 800.0); }
        public override void Reservar()
        {
            if (QuantidadeDeReservas > 0 && QuantidadeDeReservas % 1 == 0) DataReservada = ObterProximaData(DataDaUltimaReserva);
            else if (QuantidadeDeReservas == 0) DataReservada = EncontrarDataDisponivel();
            else DataReservada = DataDaUltimaReserva;
            if (DataReservada != DataDaUltimaReserva || QuantidadeDeReservas == 0) DataDaUltimaReserva = DataReservada;
            QuantidadeDeReservas++;
        }
    }

    public class StubComida { public Dictionary<string, double> Itens_Comida { get; set; } = new Dictionary<string, double>(); }

    public abstract class LocalTestBebidaBase
    {
        public Dictionary<string, double> Lista_Bebidas { get; protected set; }
        public List<int> Qtd_Bebida { get; protected set; }

        protected LocalTestBebidaBase()
        {
            Lista_Bebidas = new Dictionary<string, double>();
            Qtd_Bebida = new List<int>();
        }
        protected abstract Dictionary<string, double> SetBebidas(); 

        public virtual void DefinirQuantidades(List<int> quantidades, int numTiposBebidaDefinidos)
        {
            this.Qtd_Bebida = new List<int>(quantidades.Take(numTiposBebidaDefinidos));
            while (this.Qtd_Bebida.Count < numTiposBebidaDefinidos)
            {
                this.Qtd_Bebida.Add(0); 
            }
        }
    }

    public class LocalTestTipoBebidaConcreta : LocalTestBebidaBase
    {
        private readonly Dictionary<string, double> _definicaoDeBebidas;

        public LocalTestTipoBebidaConcreta(Dictionary<string, double> definicaoDeBebidas, List<int> quantidades) : base()
        {
            _definicaoDeBebidas = definicaoDeBebidas ?? new Dictionary<string, double>();
            this.Lista_Bebidas = SetBebidas(); 
            DefinirQuantidades(quantidades, this.Lista_Bebidas.Count); 
        }

        protected override Dictionary<string, double> SetBebidas()
        {
            this.Lista_Bebidas.Clear();
            if (_definicaoDeBebidas != null)
            {
                foreach (var kvp in _definicaoDeBebidas)
                {
                    this.Lista_Bebidas[kvp.Key] = kvp.Value;
                }
            }
            return this.Lista_Bebidas;
        }
    }

    public abstract class StubEventoBase
    {
        public virtual string TipoFesta { get; protected set; }
        public virtual Dictionary<string, double> DadosUtensilios { get; protected set; } = new Dictionary<string, double>();
        public virtual StubComida Comida { get; protected set; }
        public virtual LocalTestBebidaBase Bebida { get; protected set; }

        protected StubEventoBase()
        {
            Comida = new StubComida();
        }
    }

    public class StubCasamentoStandard : StubEventoBase
    {
        public StubCasamentoStandard(List<int> quantidadesBebidas) : base() 
        {
            TipoFesta = "CasamentoStandard";
            DadosUtensilios = new Dictionary<string, double> { { "Pratos de Jantar", 1.0 }, { "Talheres Completos", 0.5 } };
            Comida.Itens_Comida = new Dictionary<string, double> { { "Buffet Casamento Simples", 50.0 } };

            var definicaoBebidasCasamento = new Dictionary<string, double> {
                { "Água", 5.0 }, { "Suco", 8.0 }, { "Refrigerante", 7.0 }
            };
            this.Bebida = new LocalTestTipoBebidaConcreta(definicaoBebidasCasamento, quantidadesBebidas);
        }
    }

    public class StubEventoLivre : StubEventoBase
    {
        public StubEventoLivre() : base()
        {
            TipoFesta = "EventoLivre";
            this.Bebida = new LocalTestTipoBebidaConcreta(new Dictionary<string, double>(), new List<int>());
        }
    }

    public class StubCalculadoraEventos
    {
        public double CalcularComidas(StubEventoBase evento, int quantidadeParticipantes)
        {
            if (evento?.Comida?.Itens_Comida == null) return 0;
            return evento.Comida.Itens_Comida.Sum(item => item.Value * quantidadeParticipantes);
        }

        public double CalcularBebidas(StubEventoBase evento) 
        {
            if (evento?.Bebida?.Lista_Bebidas == null || evento.Bebida.Qtd_Bebida == null) return 0;
            double total = 0;
            var precos = evento.Bebida.Lista_Bebidas.Values.ToList(); 
            var qtds = evento.Bebida.Qtd_Bebida;              
            for (int i = 0; i < Math.Min(precos.Count, qtds.Count); i++)
            {
                total += precos[i] * qtds[i];
            }
            return total;
        }
        public double CalcularUtensilios(StubEventoBase evento, int quantidadeParticipantes) 
        {
            if (evento?.DadosUtensilios == null) return 0;
            return evento.DadosUtensilios.Sum(item => item.Value * quantidadeParticipantes);
        }
        public double CalcularValorTotal(StubEventoBase evento, int quantidadeParticipantes, StubReservaBase reserva)
        {
            if (evento == null || reserva?.Espaco == null) return 0;
            return CalcularComidas(evento, quantidadeParticipantes) +
                   CalcularBebidas(evento) +
                   CalcularUtensilios(evento, quantidadeParticipantes) + 
                   reserva.Espaco.Preco;
        }
    }

    public class GerenciadorFesta
    {
        public StubEventoBase GerarTipoFesta { get; set; }
        public double ValorTotalComidas { get; set; }
        public double ValorTotalBebidas { get; set; }
        public double ValorTotalUtensilios { get; set; }
        public double ValorTotal { get; set; }
        public int QuantidadeParticipantes { get; set; }
        public StubReservaBase Reservas { get; set; }
        private StubCalculadoraEventos Calculadora;

        public GerenciadorFesta(int quantidadeDeParticipantes, string tipoDeFesta, List<int> quantidadesBebidas)
        {
            this.Reservas = InstanciarReserva(quantidadeDeParticipantes);
            if (this.Reservas == null) throw new InvalidOperationException("Falha ao instanciar reserva para teste.");
            this.Reservas.Reservar();
            this.QuantidadeParticipantes = quantidadeDeParticipantes;
            this.GerarTipoFesta = InstanciarEvento(tipoDeFesta, quantidadesBebidas);
            if (this.GerarTipoFesta == null) throw new InvalidOperationException("Falha ao instanciar evento para teste.");
            this.Calculadora = new StubCalculadoraEventos();
            this.ValorTotalComidas = this.Calculadora.CalcularComidas(this.GerarTipoFesta, quantidadeDeParticipantes);
            this.ValorTotalBebidas = this.Calculadora.CalcularBebidas(this.GerarTipoFesta);
            this.ValorTotalUtensilios = this.Calculadora.CalcularUtensilios(this.GerarTipoFesta, quantidadeDeParticipantes); // Modificado
            this.ValorTotal = this.Calculadora.CalcularValorTotal(this.GerarTipoFesta, quantidadeDeParticipantes, this.Reservas);
        }
        public GerenciadorFesta(int quantidadeDeParticipantes)
        {
            this.GerarTipoFesta = new StubEventoLivre();
            this.Reservas = InstanciarReserva(quantidadeDeParticipantes);
            if (this.Reservas == null) throw new InvalidOperationException("Falha ao instanciar reserva para teste.");
            this.Reservas.Reservar();
            this.QuantidadeParticipantes = quantidadeDeParticipantes;
            if (this.Reservas.Espaco == null) throw new InvalidOperationException("Espaço da reserva não pode ser nulo para EventoLivre.");
            this.ValorTotal = this.Reservas.Espaco.Preco;
        }
        private StubEventoBase InstanciarEvento(string tipofesta, List<int> quantidadesBebidas)
        {
            switch (tipofesta)
            {
                case "CasamentoStandard": return new StubCasamentoStandard(quantidadesBebidas);
                default: throw new ArgumentException($"Tipo de festa '{tipofesta}' não suportado pelos stubs de teste.");
            }
        }
        private static StubReservaBase InstanciarReserva(int quantidadeDeParticipantes)
        {
            if (quantidadeDeParticipantes > 0 && quantidadeDeParticipantes <= 50) return new StubReservaCinquenta();
            else if (quantidadeDeParticipantes > 50 && quantidadeDeParticipantes <= 100) return new StubReservaCem();
            else if (quantidadeDeParticipantes > 100 && quantidadeDeParticipantes <= 200) return new StubReservaDuzentos();
            else if (quantidadeDeParticipantes > 200 && quantidadeDeParticipantes <= 500) return new StubReservaQuinhentos();
            else throw new ArgumentException("Capacidade do espaço inválida para stubs de teste.");
        }
        public ResumoFesta GerarResumo()
        {
            var resumo = new ResumoFesta();
            resumo.TipoFesta = this.GerarTipoFesta.TipoFesta;
            if (this.GerarTipoFesta.DadosUtensilios != null) this.GerarTipoFesta.DadosUtensilios.ToList().ForEach(item => resumo.Utensilios[item.Key] = item.Value);
            resumo.ValorTotalUtensilios = this.ValorTotalUtensilios;
            if (this.GerarTipoFesta.Comida?.Itens_Comida != null) this.GerarTipoFesta.Comida.Itens_Comida.ToList().ForEach(item => resumo.Comidas[item.Key] = item.Value);
            resumo.ValorTotalComidas = this.ValorTotalComidas;
            if (this.GerarTipoFesta.Bebida != null) resumo.Bebidas.Add(this.GerarTipoFesta.Bebida);
            resumo.ValorTotalBebidas = this.ValorTotalBebidas;
            resumo.DataReservada = this.Reservas.DataReservada;
            resumo.CapacidadeEspaco = this.Reservas.Espaco.CapacidadeDoEspaco;
            resumo.ValorEspaco = this.Reservas.Espaco.Preco;
            resumo.ValorTotalFesta = this.ValorTotal;
            return resumo;
        }
        public ResumoFesta GerarResumoEventoLivre()
        { 
            var resumo = new ResumoFesta();
            resumo.TipoFesta = this.GerarTipoFesta.TipoFesta;
            resumo.DataReservada = this.Reservas.DataReservada;
            resumo.CapacidadeEspaco = this.Reservas.Espaco.CapacidadeDoEspaco;
            resumo.ValorEspaco = this.Reservas.Espaco.Preco;
            resumo.ValorTotalFesta = this.ValorTotal;
            if (this.GerarTipoFesta.Bebida != null) resumo.Bebidas.Add(this.GerarTipoFesta.Bebida);
            return resumo;
        }
        public DadosReserva GerarDados()
        {
            var dados = new DadosReserva();
            dados.DataReservada = this.Reservas.DataReservada;
            dados.CapacidadeEspaco = this.Reservas.Espaco.CapacidadeDoEspaco;
            dados.QtdReservas = this.Reservas.QuantidadeDeReservas;
            return dados;
        }
    }

    [TestClass()]
    public class GerenciadorFestaTests
    {
        private DateTime _dataTesteBaseReservaGlobal;

        [TestInitialize]
        public void TestInitialize()
        {
            _dataTesteBaseReservaGlobal = new DateTime(2025, 10, 1);
            StubReservaBase.DataDaUltimaReserva = _dataTesteBaseReservaGlobal;
        }

        [TestMethod()]
        public void Constructor_CasamentoStandard_70Convidados_CalculaValoresCorretamente()
        {
            int qtdConvidados = 70;
            string tipoFesta = "CasamentoStandard";
            var qtdBebidas = new List<int> { 10, 5, 5 };

            double expectedValorComidas = 50.0 * qtdConvidados; 
            double expectedValorBebidas = (10 * 5.0) + (5 * 8.0) + (5 * 7.0); 
            double expectedValorUtensilios = (1.0 + 0.5) * qtdConvidados; 
            double expectedPrecoEspaco = 250.0; 
            double expectedValorTotal = expectedValorComidas + expectedValorBebidas + expectedValorUtensilios + expectedPrecoEspaco;

            var gerenciador = new GerenciadorFesta(qtdConvidados, tipoFesta, qtdBebidas);

            Assert.IsNotNull(gerenciador.Reservas);
            Assert.IsInstanceOfType(gerenciador.Reservas, typeof(StubReservaCem));
            Assert.AreEqual(_dataTesteBaseReservaGlobal, gerenciador.Reservas.DataReservada.Date);

            Assert.IsNotNull(gerenciador.GerarTipoFesta);
            Assert.IsInstanceOfType(gerenciador.GerarTipoFesta, typeof(StubCasamentoStandard));
            Assert.AreEqual(tipoFesta, gerenciador.GerarTipoFesta.TipoFesta);

            Assert.AreEqual(qtdConvidados, gerenciador.QuantidadeParticipantes);
            Assert.AreEqual(expectedValorComidas, gerenciador.ValorTotalComidas, 0.01);
            Assert.AreEqual(expectedValorBebidas, gerenciador.ValorTotalBebidas, 0.01);
            Assert.AreEqual(expectedValorUtensilios, gerenciador.ValorTotalUtensilios, 0.01);
            Assert.AreEqual(expectedValorTotal, gerenciador.ValorTotal, 0.01);
        }

        [TestMethod()]
        public void GerarResumo_CasamentoStandard_PopulaResumoCorretamente()
        {
            int qtdConvidados = 60;
            string tipoFesta = "CasamentoStandard";
            var qtdBebidas = new List<int> { 8, 6, 7 };
            var gerenciador = new GerenciadorFesta(qtdConvidados, tipoFesta, qtdBebidas);

            double expectedPrecoEspaco = 250.0; 
            double valorComidasEsperado = 50.0 * qtdConvidados;
            double valorBebidasEsperado = (8 * 5.0) + (6 * 8.0) + (7 * 7.0);
            double valorUtensiliosEsperado = (1.0 + 0.5) * qtdConvidados;
            double valorTotalEsperado = valorComidasEsperado + valorBebidasEsperado + valorUtensiliosEsperado + expectedPrecoEspaco;

            ResumoFesta resumo = gerenciador.GerarResumo();

            Assert.IsNotNull(resumo);
            Assert.AreEqual(tipoFesta, resumo.TipoFesta);
            Assert.AreEqual(_dataTesteBaseReservaGlobal, resumo.DataReservada.Date);
            Assert.AreEqual(100, resumo.CapacidadeEspaco); 
            Assert.AreEqual(expectedPrecoEspaco, resumo.ValorEspaco, 0.01);
            Assert.AreEqual(valorTotalEsperado, resumo.ValorTotalFesta, 0.01);

            Assert.AreEqual(1, resumo.Bebidas.Count, "Deveria haver um objeto Bebida na lista do resumo.");
            Assert.IsInstanceOfType(resumo.Bebidas[0], typeof(LocalTestTipoBebidaConcreta));
            var resumoBebidaConcreta = resumo.Bebidas[0] as LocalTestTipoBebidaConcreta;
            Assert.IsNotNull(resumoBebidaConcreta);
            Assert.AreEqual(3, resumoBebidaConcreta.Lista_Bebidas.Count); 
            Assert.AreEqual(qtdBebidas[0], resumoBebidaConcreta.Qtd_Bebida[0]); 
        }

        [TestMethod()]
        public void Constructor_EventoLivre_30Convidados_CalculaValorTotalCorretamente()
        {
            int qtdConvidados = 30;
            double expectedPrecoEspaco = 150.0; 

            var gerenciador = new GerenciadorFesta(qtdConvidados);

            Assert.IsNotNull(gerenciador.Reservas);
            Assert.IsInstanceOfType(gerenciador.Reservas, typeof(StubReservaCinquenta));
            Assert.AreEqual(StubReservaBase.DataDaUltimaReserva, gerenciador.Reservas.DataReservada.Date);
            Assert.IsNotNull(gerenciador.GerarTipoFesta);
            Assert.IsInstanceOfType(gerenciador.GerarTipoFesta, typeof(StubEventoLivre));
            Assert.AreEqual("EventoLivre", gerenciador.GerarTipoFesta.TipoFesta);
            Assert.AreEqual(qtdConvidados, gerenciador.QuantidadeParticipantes);
            Assert.AreEqual(expectedPrecoEspaco, gerenciador.ValorTotal, 0.01);
        }

        [TestMethod()]
        public void GerarResumoEventoLivre_PopulaResumoCorretamente()
        {
            int qtdConvidados = 45;
            var gerenciador = new GerenciadorFesta(qtdConvidados);
            double expectedPrecoEspaco = 150.0; 

            ResumoFesta resumo = gerenciador.GerarResumoEventoLivre();

            Assert.IsNotNull(resumo);
            Assert.AreEqual("EventoLivre", resumo.TipoFesta);
            Assert.AreEqual(StubReservaBase.DataDaUltimaReserva, resumo.DataReservada.Date);
            Assert.AreEqual(50, resumo.CapacidadeEspaco);
            Assert.AreEqual(expectedPrecoEspaco, resumo.ValorEspaco, 0.01);
            Assert.AreEqual(expectedPrecoEspaco, resumo.ValorTotalFesta, 0.01);
            Assert.AreEqual(1, resumo.Bebidas.Count); 
            Assert.AreEqual(0, resumo.Bebidas[0].Lista_Bebidas.Count);
        }

        [TestMethod()]
        public void GerarDados_PopulaDadosReservaCorretamente()
        {
            int qtdConvidados = 80;
            var gerenciador = new GerenciadorFesta(qtdConvidados, "CasamentoStandard", new List<int> { 1, 1, 1 });

            DadosReserva dados = gerenciador.GerarDados();

            Assert.IsNotNull(dados);
            Assert.AreEqual(StubReservaBase.DataDaUltimaReserva, dados.DataReservada.Date);
            Assert.AreEqual(100, dados.CapacidadeEspaco);
            Assert.AreEqual(1, dados.QtdReservas);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_TipoFestaInvalido_ThrowsArgumentExceptionInInstanciarEvento()
        {
            var gerenciador = new GerenciadorFesta(50, "TipoInexistenteParaTeste", new List<int>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_QtdParticipantesInvalida_ThrowsArgumentExceptionInInstanciarReserva()
        {
            var gerenciador = new GerenciadorFesta(0, "CasamentoStandard", new List<int>());
        }
    }
}