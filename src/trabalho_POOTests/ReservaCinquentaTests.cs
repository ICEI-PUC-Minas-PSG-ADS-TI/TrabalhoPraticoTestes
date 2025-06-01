using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic; 
using System.Linq;             

namespace trabalhoPOOList.Tests
{
    public interface IRepositorioReservas
    {
        DadosReservaDTO RecuperarDadosReserva(int capacidadeEspaco);
        void SalvarDadosReserva(DadosReservaDTO dados);
    }

    public class DadosReservaDTO
    {
        public DateTime DataReservada { get; set; }
        public int QtdReservas { get; set; }
        public int CapacidadeEspaco { get; set; }
    }
    public class Espaco
    {
        public int CapacidadeDoEspaco { get; protected set; }
        public double Preco { get; protected set; }

        public Espaco()
        {
            this.CapacidadeDoEspaco = 0;
            this.Preco = 0;
        }
    }

    public class EspacoCinquenta : Espaco
    {
        public EspacoCinquenta() : base()
        {
            this.CapacidadeDoEspaco = 50;
        }
    }
    public abstract class Reserva
    {
        public IRepositorioReservas repo { get; set; } 
        public Espaco Espaco { get; set; }
        public static DateTime DataDaUltimaReserva { get; set; }
        public int QuantidadeDeReservas { get; set; }
        public DateTime DataReservada { get; set; }

        protected Reserva(IRepositorioReservas repositorio) 
        {
            this.repo = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        public abstract void Reservar();
        protected abstract void SetDataReserva();

        protected virtual DateTime ObterProximaData(DateTime dataAtual)
        {
            return dataAtual.AddDays(1).Date;
        }

        protected virtual DateTime EncontrarDataDisponivel()
        {
            return Reserva.DataDaUltimaReserva.Date;
        }
    }
    public class ReservaCinquenta : Reserva 
    {
        public ReservaCinquenta(IRepositorioReservas repositorio) : base(repositorio) 
        {
            this.Espaco = new EspacoCinquenta();
            var dadosReserva = this.repo.RecuperarDadosReserva(this.Espaco.CapacidadeDoEspaco);

            if (dadosReserva != null)
            {
                Reserva.DataDaUltimaReserva = dadosReserva.DataReservada.Date;
                this.QuantidadeDeReservas = dadosReserva.QtdReservas;
            }
            else
            {
                Reserva.DataDaUltimaReserva = DateTime.Now.Date;
                this.QuantidadeDeReservas = 0;
            }
        }

        public override void Reservar()
        {
            SetDataReserva();
        }

        protected override void SetDataReserva()
        {
            DateTime dataTemp;
            if (this.QuantidadeDeReservas > 0)
            {
                dataTemp = ObterProximaData(Reserva.DataDaUltimaReserva);
            }
            else
            {
                dataTemp = EncontrarDataDisponivel();
            }
            this.DataReservada = dataTemp.Date;
            this.QuantidadeDeReservas++;
            Reserva.DataDaUltimaReserva = dataTemp.Date;
        }
    }

    public class MockRepositorioReservas : IRepositorioReservas 
    {
        private DadosReservaDTO _dadosParaRetornar; 
        public DadosReservaDTO DadosSalvosParaVerificacao { get; private set; }
        public int RecuperarChamadas { get; private set; } = 0;
        public int SalvarChamadas { get; private set; } = 0;

        public void ConfigurarRetornoRecuperar(DadosReservaDTO dados)
        {
            _dadosParaRetornar = dados;
        }

        public DadosReservaDTO RecuperarDadosReserva(int capacidadeEspaco)
        {
            RecuperarChamadas++;
            if (_dadosParaRetornar != null && _dadosParaRetornar.CapacidadeEspaco == capacidadeEspaco)
            {
                return _dadosParaRetornar;
            }
            return null;
        }

        public void SalvarDadosReserva(DadosReservaDTO dados)
        {
            SalvarChamadas++;
            DadosSalvosParaVerificacao = dados;
        }

        public void Reset()
        {
            _dadosParaRetornar = null;
            DadosSalvosParaVerificacao = null;
            RecuperarChamadas = 0;
            SalvarChamadas = 0;
        }
    }

    [TestClass()]
    public class ReservaCinquentaTests
    {
        private MockRepositorioReservas _mockRepo;
        private DateTime _dataTesteBase;
        private const int CAPACIDADE_ESPACO_CINQUENTA = 50;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new MockRepositorioReservas(); 
            _dataTesteBase = new DateTime(2025, 7, 1).Date;
            Reserva.DataDaUltimaReserva = _dataTesteBase; 
        }

        [TestMethod()]
        public void Constructor_QuandoRepositorioRetornaDados_InicializaCorretamente()
        {
            var dadosRepo = new DadosReservaDTO 
            {
                DataReservada = _dataTesteBase.AddDays(3),
                QtdReservas = 2,
                CapacidadeEspaco = CAPACIDADE_ESPACO_CINQUENTA
            };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);

            var reservaCinquenta = new ReservaCinquenta(_mockRepo); 

            Assert.AreEqual(CAPACIDADE_ESPACO_CINQUENTA, reservaCinquenta.Espaco.CapacidadeDoEspaco);
            Assert.AreEqual(dadosRepo.DataReservada.Date, Reserva.DataDaUltimaReserva.Date);
            Assert.AreEqual(dadosRepo.QtdReservas, reservaCinquenta.QuantidadeDeReservas);
            Assert.AreEqual(1, _mockRepo.RecuperarChamadas);
        }

        [TestMethod()]
        public void Constructor_QuandoRepositorioRetornaNull_InicializaComValoresPadrao()
        {
            _mockRepo.ConfigurarRetornoRecuperar(null);
            DateTime dataEsperadaAgora = DateTime.Now.Date;

            var reservaCinquenta = new ReservaCinquenta(_mockRepo);

            Assert.AreEqual(CAPACIDADE_ESPACO_CINQUENTA, reservaCinquenta.Espaco.CapacidadeDoEspaco);
            Assert.AreEqual(dataEsperadaAgora, Reserva.DataDaUltimaReserva.Date);
            Assert.AreEqual(0, reservaCinquenta.QuantidadeDeReservas);
            Assert.AreEqual(1, _mockRepo.RecuperarChamadas);
        }

        [TestMethod()]
        public void Reservar_PrimeiraReservaAposInicializacaoSemDadosRepo_UsaDataAtualEIncrementaQuantidade()
        {
            _mockRepo.ConfigurarRetornoRecuperar(null);
            var reservaCinquenta = new ReservaCinquenta(_mockRepo);
            DateTime dataEsperada = DateTime.Now.Date;

            reservaCinquenta.Reservar();

            Assert.AreEqual(dataEsperada, reservaCinquenta.DataReservada.Date);
            Assert.AreEqual(1, reservaCinquenta.QuantidadeDeReservas);
            Assert.AreEqual(dataEsperada, Reserva.DataDaUltimaReserva.Date);
        }

        [TestMethod()]
        public void Reservar_PrimeiraReservaAposInicializacaoComDadosRepoZerados_UsaDataDoRepoEIncrementaQuantidade()
        {
            var dadosRepo = new DadosReservaDTO
            {
                DataReservada = _dataTesteBase,
                QtdReservas = 0,
                CapacidadeEspaco = CAPACIDADE_ESPACO_CINQUENTA
            };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
            var reservaCinquenta = new ReservaCinquenta(_mockRepo);

            reservaCinquenta.Reservar();

            Assert.AreEqual(_dataTesteBase, reservaCinquenta.DataReservada.Date);
            Assert.AreEqual(1, reservaCinquenta.QuantidadeDeReservas);
            Assert.AreEqual(_dataTesteBase, Reserva.DataDaUltimaReserva.Date);
        }

        [TestMethod()]
        public void Reservar_SegundaReserva_AvancaParaProximaData()
        {
            var dadosRepo = new DadosReservaDTO
            {
                DataReservada = _dataTesteBase,
                QtdReservas = 1,
                CapacidadeEspaco = CAPACIDADE_ESPACO_CINQUENTA
            };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
            var reservaCinquenta = new ReservaCinquenta(_mockRepo);
            DateTime proximaDataEsperada = _dataTesteBase.AddDays(1);

            reservaCinquenta.Reservar();

            Assert.AreEqual(proximaDataEsperada, reservaCinquenta.DataReservada.Date);
            Assert.AreEqual(2, reservaCinquenta.QuantidadeDeReservas);
            Assert.AreEqual(proximaDataEsperada, Reserva.DataDaUltimaReserva.Date);
        }

        [TestMethod()]
        public void Reservar_TerceiraReserva_AvancaDataNovamente()
        {
            DateTime dataDaSegundaReserva = _dataTesteBase.AddDays(1);
            var dadosRepo = new DadosReservaDTO
            {
                DataReservada = dataDaSegundaReserva,
                QtdReservas = 2,
                CapacidadeEspaco = CAPACIDADE_ESPACO_CINQUENTA
            };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
            var reservaCinquenta = new ReservaCinquenta(_mockRepo);
            DateTime proximaDataEsperada = dataDaSegundaReserva.AddDays(1);

            reservaCinquenta.Reservar();

            Assert.AreEqual(proximaDataEsperada, reservaCinquenta.DataReservada.Date);
            Assert.AreEqual(3, reservaCinquenta.QuantidadeDeReservas);
            Assert.AreEqual(proximaDataEsperada, Reserva.DataDaUltimaReserva.Date);
        }

        [TestMethod()]
        public void Reservar_AposVariasReservas_SequenciaDeDatasCorreta()
        {
            _mockRepo.ConfigurarRetornoRecuperar(null);
            var reservaCinquenta = new ReservaCinquenta(_mockRepo);
            DateTime dataBaseParaSequencia = DateTime.Now.Date;

            reservaCinquenta.Reservar();
            Assert.AreEqual(dataBaseParaSequencia, reservaCinquenta.DataReservada.Date, "Falha na 1ª reserva");
            Assert.AreEqual(1, reservaCinquenta.QuantidadeDeReservas, "Falha na Qtd após 1ª reserva");
            Assert.AreEqual(dataBaseParaSequencia, Reserva.DataDaUltimaReserva.Date, "Falha na DataDaUltimaReserva após 1ª reserva");

            reservaCinquenta.Reservar();
            DateTime dataSegundaEsperada = dataBaseParaSequencia.AddDays(1);
            Assert.AreEqual(dataSegundaEsperada, reservaCinquenta.DataReservada.Date, "Falha na 2ª reserva");
            Assert.AreEqual(2, reservaCinquenta.QuantidadeDeReservas, "Falha na Qtd após 2ª reserva");
            Assert.AreEqual(dataSegundaEsperada, Reserva.DataDaUltimaReserva.Date, "Falha na DataDaUltimaReserva após 2ª reserva");

            reservaCinquenta.Reservar();
            DateTime dataTerceiraEsperada = dataBaseParaSequencia.AddDays(2);
            Assert.AreEqual(dataTerceiraEsperada, reservaCinquenta.DataReservada.Date, "Falha na 3ª reserva");
            Assert.AreEqual(3, reservaCinquenta.QuantidadeDeReservas, "Falha na Qtd após 3ª reserva");
            Assert.AreEqual(dataTerceiraEsperada, Reserva.DataDaUltimaReserva.Date, "Falha na DataDaUltimaReserva após 3ª reserva");
        }
    }
}