using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic; 
using System.Linq;            

namespace trabalhoPOOList.Tests
{
    public class EspacoCem : Espaco
    {
        public EspacoCem() : base()
        {
            this.CapacidadeDoEspaco = 100;
        }
    }
    public class ReservaCem : Reserva 
    {
        public ReservaCem(IRepositorioReservas repositorio) : base(repositorio) 
        {
            this.Espaco = new EspacoCem(); 
            if (this.repo != null)
            {
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

            if (this.QuantidadeDeReservas % 4 == 0 && this.QuantidadeDeReservas > 0)
            {
                dataTemp = ObterProximaData(Reserva.DataDaUltimaReserva);
                Reserva.DataDaUltimaReserva = dataTemp; 
            }
            else if (this.QuantidadeDeReservas == 0) 
            {
                dataTemp = EncontrarDataDisponivel(); 
                Reserva.DataDaUltimaReserva = dataTemp; 
            }
            else 
            {
                dataTemp = Reserva.DataDaUltimaReserva;
            }

            this.DataReservada = dataTemp.Date;
            this.QuantidadeDeReservas++;
        }
    }
    [TestClass()]
    public class ReservaCemTests
    {
        private MockRepositorioReservas _mockRepo; 
        private DateTime _dataTesteBase;
        private const int CAPACIDADE_ESPACO_CEM = 100;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new MockRepositorioReservas();
            _dataTesteBase = new DateTime(2025, 6, 1).Date;
            Reserva.DataDaUltimaReserva = _dataTesteBase;
        }

        [TestMethod()]
        public void Constructor_QuandoRepositorioRetornaDados_InicializaCorretamente()
        {
            var dadosRepo = new DadosReservaDTO 
            {
                DataReservada = _dataTesteBase.AddDays(5),
                QtdReservas = 2,
                CapacidadeEspaco = CAPACIDADE_ESPACO_CEM
            };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);

            var reservaCem = new ReservaCem(_mockRepo); 

            Assert.IsNotNull(reservaCem.Espaco, "A propriedade Espaco não foi inicializada.");
            Assert.AreEqual(CAPACIDADE_ESPACO_CEM, reservaCem.Espaco.CapacidadeDoEspaco);
            Assert.AreEqual(dadosRepo.DataReservada.Date, Reserva.DataDaUltimaReserva.Date);
            Assert.AreEqual(dadosRepo.QtdReservas, reservaCem.QuantidadeDeReservas);
            Assert.AreEqual(1, _mockRepo.RecuperarChamadas);
        }

        [TestMethod()]
        public void Constructor_QuandoRepositorioRetornaNull_InicializaComValoresPadrao()
        {
            _mockRepo.ConfigurarRetornoRecuperar(null);
            DateTime dataEsperadaAgora = DateTime.Now.Date;

            var reservaCem = new ReservaCem(_mockRepo);

            Assert.IsNotNull(reservaCem.Espaco, "A propriedade Espaco não foi inicializada.");
            Assert.AreEqual(CAPACIDADE_ESPACO_CEM, reservaCem.Espaco.CapacidadeDoEspaco);
            Assert.AreEqual(dataEsperadaAgora, Reserva.DataDaUltimaReserva.Date);
            Assert.AreEqual(0, reservaCem.QuantidadeDeReservas);
            Assert.AreEqual(1, _mockRepo.RecuperarChamadas);
        }

        [TestMethod()]
        public void Reservar_PrimeiraReservaAposInicializacaoSemDadosRepo_UsaDataAtualEIncrementaQuantidade()
        {
            _mockRepo.ConfigurarRetornoRecuperar(null);
            var reservaCem = new ReservaCem(_mockRepo);
            DateTime dataEsperada = DateTime.Now.Date;

            reservaCem.Reservar();

            Assert.AreEqual(dataEsperada, reservaCem.DataReservada.Date);
            Assert.AreEqual(1, reservaCem.QuantidadeDeReservas);
            Assert.AreEqual(dataEsperada, Reserva.DataDaUltimaReserva.Date);
        }

        [TestMethod()]
        public void Reservar_PrimeiraReservaAposInicializacaoComDadosRepo_UsaDataDoRepoEIncrementaQuantidade()
        {
            var dadosRepo = new DadosReservaDTO
            {
                DataReservada = _dataTesteBase,
                QtdReservas = 0,
                CapacidadeEspaco = CAPACIDADE_ESPACO_CEM
            };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
            var reservaCem = new ReservaCem(_mockRepo);

            reservaCem.Reservar();

            Assert.AreEqual(_dataTesteBase, reservaCem.DataReservada.Date);
            Assert.AreEqual(1, reservaCem.QuantidadeDeReservas);
            Assert.AreEqual(_dataTesteBase, Reserva.DataDaUltimaReserva.Date);
        }

        [TestMethod()]
        public void Reservar_SegundaReservaNaMesmaData_UsaMesmaDataEIncrementaQuantidade()
        {
            var dadosRepo = new DadosReservaDTO { DataReservada = _dataTesteBase, QtdReservas = 1, CapacidadeEspaco = CAPACIDADE_ESPACO_CEM };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
            var reservaCem = new ReservaCem(_mockRepo);

            reservaCem.Reservar();

            Assert.AreEqual(_dataTesteBase, reservaCem.DataReservada.Date);
            Assert.AreEqual(2, reservaCem.QuantidadeDeReservas);
            Assert.AreEqual(_dataTesteBase, Reserva.DataDaUltimaReserva.Date);
        }

        [TestMethod()]
        public void Reservar_QuartaReservaNaMesmaData_UsaMesmaDataEIncrementaQuantidade()
        {
            var dadosRepo = new DadosReservaDTO { DataReservada = _dataTesteBase, QtdReservas = 3, CapacidadeEspaco = CAPACIDADE_ESPACO_CEM };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
            var reservaCem = new ReservaCem(_mockRepo); 

            reservaCem.Reservar(); 

            Assert.AreEqual(_dataTesteBase, reservaCem.DataReservada.Date);
            Assert.AreEqual(4, reservaCem.QuantidadeDeReservas);
            Assert.AreEqual(_dataTesteBase, Reserva.DataDaUltimaReserva.Date);
        }

        [TestMethod()]
        public void Reservar_QuintaReserva_AvancaParaProximaDataEIncrementaQuantidade()
        {
            var dadosRepo = new DadosReservaDTO { DataReservada = _dataTesteBase, QtdReservas = 4, CapacidadeEspaco = CAPACIDADE_ESPACO_CEM };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
            var reservaCem = new ReservaCem(_mockRepo);
            DateTime proximaDataEsperada = _dataTesteBase.AddDays(1);

            reservaCem.Reservar(); 

            Assert.AreEqual(proximaDataEsperada, reservaCem.DataReservada.Date);
            Assert.AreEqual(5, reservaCem.QuantidadeDeReservas);
            Assert.AreEqual(proximaDataEsperada, Reserva.DataDaUltimaReserva.Date);
        }

        [TestMethod()]
        public void Reservar_OitavaReserva_MantemNaSegundaDataEIncrementaQuantidade()
        {
            DateTime dataDia2 = _dataTesteBase.AddDays(1);
            var dadosRepo = new DadosReservaDTO { DataReservada = dataDia2, QtdReservas = 7, CapacidadeEspaco = CAPACIDADE_ESPACO_CEM };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
            var reservaCem = new ReservaCem(_mockRepo); 

            reservaCem.Reservar();

            Assert.AreEqual(dataDia2, reservaCem.DataReservada.Date);
            Assert.AreEqual(8, reservaCem.QuantidadeDeReservas);
            Assert.AreEqual(dataDia2, Reserva.DataDaUltimaReserva.Date);
        }

        [TestMethod()]
        public void Reservar_NonaReserva_AvancaParaTerceiraDataEIncrementaQuantidade()
        {
            DateTime dataDia2 = _dataTesteBase.AddDays(1);
            var dadosRepo = new DadosReservaDTO { DataReservada = dataDia2, QtdReservas = 8, CapacidadeEspaco = CAPACIDADE_ESPACO_CEM };
            _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
            var reservaCem = new ReservaCem(_mockRepo);

            DateTime proximaDataEsperada = dataDia2.AddDays(1); 

            reservaCem.Reservar(); 

            Assert.AreEqual(proximaDataEsperada, reservaCem.DataReservada.Date);
            Assert.AreEqual(9, reservaCem.QuantidadeDeReservas);
            Assert.AreEqual(proximaDataEsperada, Reserva.DataDaUltimaReserva.Date);
        }
    }
}