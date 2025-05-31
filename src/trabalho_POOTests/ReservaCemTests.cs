using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic; // Required for List
using System.Linq; // Required for LINQ methods if used
using Trabalho_POO;
using trabalhoPOOList;
// --- Namespaces for main project classes ---
// The definitions for IRepositorioReservas and DadosReservaDTO should exist
// in your main project under the 'Trabalho_POO' namespace.
// We will remove their duplicate definitions from this test file.
namespace Trabalho_POO
{
    // --- Interfaces and DTOs ---
    // REMOVED: public interface IRepositorioReservas { ... }
    // REMOVED: public class DadosReservaDTO { ... }
    // These should be defined in your main project (e.g., in a file like IRepositorioReservas.cs)

    // --- Stub/Base Classes (designed for testability) ---
    // Added to allow Reserva to use trabalhoPOOList.Espaco

    //    public abstract class Reserva
    //    {
    //        // Modified to allow setting by tests in the same assembly
    //        public IRepositorioReservas repo { get; protected internal set; }
    //        public Espaco Espaco { get; protected set; }
    //        public static DateTime DataDaUltimaReserva { get; set; }
    //        public int QuantidadeDeReservas { get; set; }
    //        public DateTime DataReservada { get; protected set; }

    //        // Parameterless constructor for derived classes like the new ReservaCem
    //        protected Reserva() { }

    //        // Original constructor for dependency injection (can still be used by other derived classes)
    //        protected Reserva(IRepositorioReservas repositorio)
    //        {
    //            this.repo = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
    //        }

    //        public abstract void Reservar();
    //        protected abstract void SetDataReserva();

    //        protected virtual DateTime ObterProximaData(DateTime dataAtual)
    //        {
    //            return dataAtual.AddDays(1).Date;
    //        }

    //        protected virtual DateTime EncontrarDataDisponivel()
    //        {
    //            return Reserva.DataDaUltimaReserva.Date;
    //        }
    //    }
    //}

    namespace trabalhoPOOList
    {
        using System;


        //public class Espaco // cria um objeto espaco com atributos comuns para uso das classes derivadas
        //{
        //    public int CapacidadeDoEspaco { get; protected set; }
        //    public double Preco { get; protected set; }

        //    public Espaco()
        //    {
        //        this.CapacidadeDoEspaco = 0;
        //        this.Preco = 0;
        //    }
        //}

        //public class EspacoCem : Espaco
        //{
        //    public EspacoCem() : base()
        //    {
        //        this.CapacidadeDoEspaco = 100;
        //        // this.Preco = 250.0; 
        //    }
        //}

        // User's new ReservaCem class
        public class ReservaCem : Reserva
        {
            public ReservaCem(IRepositorioReservas repositorio) : base(repositorio) // Parameterless constructor
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
                else if (this.QuantidadeDeReservas > 0)
                {
                    dataTemp = Reserva.DataDaUltimaReserva;
                }
                else
                {
                    dataTemp = EncontrarDataDisponivel();
                    Reserva.DataDaUltimaReserva = dataTemp;
                }
                this.DataReservada = dataTemp.Date;
                this.QuantidadeDeReservas++;

                // Consider saving state:
                // if (this.repo != null)
                // {
                //    this.repo.SalvarDadosReserva(new DadosReservaDTO { /* ... */ });
                // }
            }
        }
    }

    // --- Test Project Code (trabalhoPOOList.Tests) ---
    namespace trabalhoPOOList.Tests
    {
        using Microsoft.VisualStudio.TestTools.UnitTesting;
        using System;
        using Trabalho_POO; // This using statement allows access to IRepositorioReservas and DadosReservaDTO from the main project
        using trabalhoPOOList;

        public class MockRepositorioReservas : IRepositorioReservas // Implements the interface from Trabalho_POO
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
        public class ReservaCemTests
        {
            private MockRepositorioReservas _mockRepo;
            private DateTime _dataTesteBase;

            [TestInitialize]
            public void TestInitialize()
            {
                _mockRepo = new MockRepositorioReservas();
                _dataTesteBase = new DateTime(2025, 6, 1).Date; // Use a fixed date for predictable tests
                Reserva.DataDaUltimaReserva = _dataTesteBase; // Reset static variable
            }

            [TestMethod()]
            public void Constructor_QuandoRepositorioRetornaDados_InicializaCorretamente()
            {
                // Arrange
                var dadosRepo = new DadosReservaDTO
                {
                    DataReservada = _dataTesteBase.AddDays(5),
                    QtdReservas = 2,
                    CapacidadeEspaco = 100
                };
                _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);

                // Act
                var reservaCem = new ReservaCem(_mockRepo);

                // Assert
                Assert.AreEqual(100, reservaCem.Espaco.CapacidadeDoEspaco);
                Assert.AreEqual(dadosRepo.DataReservada.Date, Reserva.DataDaUltimaReserva.Date);
                Assert.AreEqual(dadosRepo.QtdReservas, reservaCem.QuantidadeDeReservas);
                Assert.AreEqual(1, _mockRepo.RecuperarChamadas);
            }

            [TestMethod()]
            public void Constructor_QuandoRepositorioRetornaNull_InicializaComValoresPadrao()
            {
                // Arrange
                _mockRepo.ConfigurarRetornoRecuperar(null);
                // Static DataDaUltimaReserva will be set to DateTime.Now.Date by ReservaCem constructor
                // For test predictability, we can check against that.
                DateTime dataEsperadaAgora = DateTime.Now.Date;

                // Act
                var reservaCem = new ReservaCem(_mockRepo);

                // Assert
                Assert.AreEqual(100, reservaCem.Espaco.CapacidadeDoEspaco);
                Assert.AreEqual(dataEsperadaAgora, Reserva.DataDaUltimaReserva.Date);
                Assert.AreEqual(0, reservaCem.QuantidadeDeReservas);
                Assert.AreEqual(1, _mockRepo.RecuperarChamadas);
            }

            [TestMethod()]
            public void Reservar_PrimeiraReservaAposInicializacaoSemDadosRepo_UsaDataAtualEIncrementaQuantidade()
            {
                // Arrange
                _mockRepo.ConfigurarRetornoRecuperar(null); // No prior data from repo
                var reservaCem = new ReservaCem(_mockRepo); // QtdReservas = 0, DataDaUltimaReserva = Today

                DateTime dataEsperada = DateTime.Now.Date;

                // Act
                reservaCem.Reservar();

                // Assert
                Assert.AreEqual(dataEsperada, reservaCem.DataReservada.Date);
                Assert.AreEqual(1, reservaCem.QuantidadeDeReservas);
                Assert.AreEqual(dataEsperada, Reserva.DataDaUltimaReserva.Date); // Static date should also be updated
            }

            [TestMethod()]
            public void Reservar_PrimeiraReservaAposInicializacaoComDadosRepo_UsaDataDoRepoEIncrementaQuantidade()
            {
                // Arrange
                var dadosRepo = new DadosReservaDTO
                {
                    DataReservada = _dataTesteBase, // Last reservation was on _dataTesteBase
                    QtdReservas = 0, // No reservations on that date yet according to QtdReservas
                    CapacidadeEspaco = 100
                };
                _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
                var reservaCem = new ReservaCem(_mockRepo); // QtdReservas = 0, DataDaUltimaReserva = _dataTesteBase

                // Act
                reservaCem.Reservar(); // Should reserve on _dataTesteBase

                // Assert
                Assert.AreEqual(_dataTesteBase, reservaCem.DataReservada.Date);
                Assert.AreEqual(1, reservaCem.QuantidadeDeReservas);
                Assert.AreEqual(_dataTesteBase, Reserva.DataDaUltimaReserva.Date);
            }


            [TestMethod()]
            public void Reservar_SegundaReservaNaMesmaData_UsaMesmaDataEIncrementaQuantidade()
            {
                // Arrange
                // Initial state: 1 reservation already made on _dataTesteBase
                var dadosRepo = new DadosReservaDTO { DataReservada = _dataTesteBase, QtdReservas = 1, CapacidadeEspaco = 100 };
                _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
                var reservaCem = new ReservaCem(_mockRepo); // QtdReservas = 1, DataDaUltimaReserva = _dataTesteBase

                // Act
                reservaCem.Reservar(); // This will be the 2nd reservation

                // Assert
                Assert.AreEqual(_dataTesteBase, reservaCem.DataReservada.Date); // Should still be on _dataTesteBase
                Assert.AreEqual(2, reservaCem.QuantidadeDeReservas);
                Assert.AreEqual(_dataTesteBase, Reserva.DataDaUltimaReserva.Date);
            }

            [TestMethod()]
            public void Reservar_QuartaReservaNaMesmaData_UsaMesmaDataEIncrementaQuantidade()
            {
                // Arrange
                // Initial state: 3 reservations on _dataTesteBase
                var dadosRepo = new DadosReservaDTO { DataReservada = _dataTesteBase, QtdReservas = 3, CapacidadeEspaco = 100 };
                _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
                var reservaCem = new ReservaCem(_mockRepo); // QtdReservas = 3, DataDaUltimaReserva = _dataTesteBase

                // Act
                reservaCem.Reservar(); // This will be the 4th reservation

                // Assert
                Assert.AreEqual(_dataTesteBase, reservaCem.DataReservada.Date); // Should still be on _dataTesteBase
                Assert.AreEqual(4, reservaCem.QuantidadeDeReservas);
                Assert.AreEqual(_dataTesteBase, Reserva.DataDaUltimaReserva.Date);
            }

            [TestMethod()]
            public void Reservar_QuintaReserva_AvancaParaProximaDataEIncrementaQuantidade()
            {
                // Arrange
                // Initial state: 4 reservations on _dataTesteBase (date is full)
                var dadosRepo = new DadosReservaDTO { DataReservada = _dataTesteBase, QtdReservas = 4, CapacidadeEspaco = 100 };
                _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
                var reservaCem = new ReservaCem(_mockRepo); // QtdReservas = 4, DataDaUltimaReserva = _dataTesteBase

                DateTime proximaDataEsperada = _dataTesteBase.AddDays(1);

                // Act
                reservaCem.Reservar(); // This will be the 5th reservation overall, 1st on the new date

                // Assert
                Assert.AreEqual(proximaDataEsperada, reservaCem.DataReservada.Date); // Should be on the next day
                Assert.AreEqual(5, reservaCem.QuantidadeDeReservas);
                Assert.AreEqual(proximaDataEsperada, Reserva.DataDaUltimaReserva.Date); // Static date updated
            }

            [TestMethod()]
            public void Reservar_OitavaReserva_MantemNaSegundaDataEIncrementaQuantidade()
            {
                // Arrange
                // Initial state: 7 reservations. (4 on day1, 3 on day2)
                // So, DataDaUltimaReserva should be day2 (_dataTesteBase.AddDays(1))
                // And QuantidadeDeReservas from repo is 7.
                DateTime dataDia2 = _dataTesteBase.AddDays(1);
                var dadosRepo = new DadosReservaDTO { DataReservada = dataDia2, QtdReservas = 7, CapacidadeEspaco = 100 };
                _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
                var reservaCem = new ReservaCem(_mockRepo); // QtdReservas = 7, DataDaUltimaReserva = dataDia2

                // Act
                reservaCem.Reservar(); // This will be the 8th reservation (4th on dataDia2)

                // Assert
                Assert.AreEqual(dataDia2, reservaCem.DataReservada.Date); // Should still be on dataDia2
                Assert.AreEqual(8, reservaCem.QuantidadeDeReservas);
                Assert.AreEqual(dataDia2, Reserva.DataDaUltimaReserva.Date);
            }

            [TestMethod()]
            public void Reservar_NonaReserva_AvancaParaTerceiraDataEIncrementaQuantidade()
            {
                // Arrange
                // Initial state: 8 reservations. (4 on day1, 4 on day2).
                // DataDaUltimaReserva should be day2 (_dataTesteBase.AddDays(1))
                // QuantidadeDeReservas from repo is 8.
                DateTime dataDia2 = _dataTesteBase.AddDays(1);
                var dadosRepo = new DadosReservaDTO { DataReservada = dataDia2, QtdReservas = 8, CapacidadeEspaco = 100 };
                _mockRepo.ConfigurarRetornoRecuperar(dadosRepo);
                var reservaCem = new ReservaCem(_mockRepo); // QtdReservas = 8, DataDaUltimaReserva = dataDia2

                DateTime proximaDataEsperada = dataDia2.AddDays(1); // This would be _dataTesteBase.AddDays(2)

                // Act
                reservaCem.Reservar(); // This will be the 9th reservation (1st on dataDia3)

                // Assert
                Assert.AreEqual(proximaDataEsperada, reservaCem.DataReservada.Date);
                Assert.AreEqual(9, reservaCem.QuantidadeDeReservas);
                Assert.AreEqual(proximaDataEsperada, Reserva.DataDaUltimaReserva.Date);
            }
        }
    }
}

