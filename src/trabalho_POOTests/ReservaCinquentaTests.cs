using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic; // Required for List
using System.Linq; // Required for LINQ methods if used
using trabalhoPOOList;
// --- Namespaces for main project classes ---
namespace Trabalho_POO
{
    // --- Interfaces and DTOs ---
    public interface IRepositorioReservas
    {
        DadosReservaDTO RecuperarDadosReserva(int capacidadeEspaco);
        void SalvarDadosReserva(DadosReservaDTO dados); // Added for completeness
    }

    public class DadosReservaDTO
    {
        public DateTime DataReservada { get; set; }
        public int QtdReservas { get; set; }
        public int CapacidadeEspaco { get; set; }
    }

    // --- Stub/Base Classes (designed for testability) ---
    // The Espaco class is now defined in the 'trabalhoPOOList' namespace.
    // The Reserva class needs to use it.
    // Added to allow Reserva to use trabalhoPOOList.Espaco

    public abstract class Reserva
    {
        public IRepositorioReservas repo { get; set; }
        public Espaco Espaco { get; set; } // This will now refer to trabalhoPOOList.Espaco
        public static DateTime DataDaUltimaReserva { get; set; } // Static member, needs care in tests
        public int QuantidadeDeReservas { get; set; }
        public DateTime DataReservada { get; set; }

        // Constructor for dependency injection
        protected Reserva(IRepositorioReservas repositorio)
        {
            this.repo = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        // Abstract methods to be implemented by derived classes
        public abstract void Reservar();
        protected abstract void SetDataReserva();

        // Virtual methods that can be used by derived classes or overridden in tests if complex
        protected virtual DateTime ObterProximaData(DateTime dataAtual)
        {
            // Simple stub: returns next day.
            // In a real scenario, this might have more complex business logic (e.g., skip weekends).
            return dataAtual.AddDays(1).Date;
        }

        protected virtual DateTime EncontrarDataDisponivel()
        {
            // If QuantidadeDeReservas is 0, it means we are finding a date for the very first slot.
            // The constructor of ReservaCem sets DataDaUltimaReserva based on repo data or DateTime.Now.Date.
            // This method should return that initial date for the first reservation.
            return Reserva.DataDaUltimaReserva.Date;
        }
    }
}

namespace trabalhoPOOList // Assuming ReservaCem and new Espaco/EspacoCem are in this namespace
{
    using System;
    using Trabalho_POO; // For base Reserva, IRepositorioReservas

    // User's updated Espaco class definition
    public class Espaco // cria um objeto espaco com atributos comuns para uso das classes derivadas
    {
        public int CapacidadeDoEspaco { get; protected set; }
        public double Preco { get; protected set; }

        public Espaco()
        {
            this.CapacidadeDoEspaco = 0;
            this.Preco = 0;
        }
    }

    // Updated EspacoCem to inherit from the new Espaco class in this namespace
    public class EspacoCem : Espaco
    {
        public EspacoCem() : base() // Calls Espaco's constructor
        {
            this.CapacidadeDoEspaco = 100;
            // this.Preco = 250.0; // Set if needed, base Espaco initializes Preco to 0
        }
    }

    public class ReservaCem : Reserva
    {
        // Modified constructor to accept IRepositorioReservas for testability
        public ReservaCem(IRepositorioReservas repositorio) : base(repositorio)
        {
            this.Espaco = new EspacoCem(); // EspacoCem defines CapacidadeDoEspaco = 100

            // 'this.repo' is now set by the base class constructor
            var dadosReserva = this.repo.RecuperarDadosReserva(this.Espaco.CapacidadeDoEspaco);

            if (dadosReserva != null)
            {
                Reserva.DataDaUltimaReserva = dadosReserva.DataReservada.Date;
                this.QuantidadeDeReservas = dadosReserva.QtdReservas;
            }
            else
            {
                // If no data from repo, initialize for the first time
                Reserva.DataDaUltimaReserva = DateTime.Now.Date; // Use .Date for consistency
                this.QuantidadeDeReservas = 0;
            }
        }

        public override void Reservar()
        {
            SetDataReserva();
        }

        // Escolhe realiza o agendamento na data apropriada
        // regra de negócio ( espaço para cem pessoas 4 agendamentos por data)
        protected override void SetDataReserva()
        {
            DateTime dataTemp;

            if (this.QuantidadeDeReservas % 4 == 0 && this.QuantidadeDeReservas > 0)
            {
                // Current date is full, get the next available date
                dataTemp = ObterProximaData(Reserva.DataDaUltimaReserva);
                Reserva.DataDaUltimaReserva = dataTemp; // Update static last reservation date
            }
            else if (this.QuantidadeDeReservas > 0)
            {
                // There are existing reservations, continue on the same last known date
                dataTemp = Reserva.DataDaUltimaReserva;
            }
            else // QuantidadeDeReservas == 0 (this is the very first reservation for this instance)
            {
                // Find an initial available date (uses DataDaUltimaReserva which was set in constructor)
                dataTemp = EncontrarDataDisponivel();
                Reserva.DataDaUltimaReserva = dataTemp; // Ensure static date is set for the first reservation
            }

            this.DataReservada = dataTemp.Date; // Store only the date part
            this.QuantidadeDeReservas++;

            // Consider saving the state back to the repository here if needed
            // this.repo.SalvarDadosReserva(new DadosReservaDTO {
            //    DataReservada = this.DataReservada,
            //    QtdReservas = this.QuantidadeDeReservas,
            //    CapacidadeEspaco = this.Espaco.CapacidadeDoEspaco
            // });
        }
    }
}

// --- Test Project Code (trabalhoPOOList.Tests) ---
namespace trabalhoPOOList.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Trabalho_POO; // For IRepositorioReservas, DadosReservaDTO
    using trabalhoPOOList; // For ReservaCem, EspacoCem, Espaco

    // Mock Repository Implementation for testing
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
            // Only return data if it's for the correct capacity (100 for EspacoCem)
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
            // This mock currently doesn't store multiple saves, just the last one.
            // For more complex scenarios, you might store a list of saved DTOs.
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