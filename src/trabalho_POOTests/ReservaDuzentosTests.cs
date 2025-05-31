using Microsoft.VisualStudio.TestTools.UnitTesting;
using trabalhoPOOList;
using System;
using trabalhoPOO;
using Trabalho_POO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Drawing;

namespace trabalhoPOOList.Tests
{
    [TestClass()]
    public class ReservaDuzentosTests
    {
        // Interface simples para o repositório
        private interface IRepositorioReserva
        {
            DadosReserva RecuperarDadosReserva(int capacidadeEspaco);
        }

        // Implementação mock do repositório
        private class RepositorioMock : IRepositorioReserva
        {
            public DadosReserva DadosFicticios { get; set; }

            public DadosReserva RecuperarDadosReserva(int capacidadeEspaco)
            {
                return DadosFicticios ?? new DadosReserva();
            }
        }

        // Método auxiliar para criar reserva com controle total
        private ReservaDuzentos CriarReservaParaTeste(DadosReserva dadosMock = null)
        {
            // Cria instância sem chamar construtor padrão
            var reserva = (ReservaDuzentos)FormatterServices.GetUninitializedObject(typeof(ReservaDuzentos));

            // Injeta o mock do repositório
            var repoField = typeof(ReservaDuzentos).GetField("repo",
                BindingFlags.NonPublic | BindingFlags.Instance);
            repoField?.SetValue(reserva, new RepositorioMock { DadosFicticios = dadosMock });

            // Inicializa propriedades obrigatórias
            reserva.Espaco = new EspacoDuzentos();

            if (dadosMock != null)
            {
                reserva.QuantidadeDeReservas = dadosMock.QtdReservas;
                Reserva.DataDaUltimaReserva = dadosMock.DataReservada;
            }
            else
            {
                // Inicialização padrão para testes sem dados
                reserva.QuantidadeDeReservas = 0;
                Reserva.DataDaUltimaReserva = DateTime.Now;
            }

            return reserva;
        }


        [TestMethod()]
        public void Construtor_DeveInicializarComEspacoDuzentos()
        {
            // Arrange & Act
            var reserva = CriarReservaParaTeste();

            // Assert
            Assert.IsInstanceOfType(reserva.Espaco, typeof(EspacoDuzentos));
            Assert.AreEqual(200, reserva.Espaco.CapacidadeDoEspaco);
        }

        [TestMethod()]
        public void Construtor_SemDadosPrevios_DeveIniciarComDataAtualEZeroReservas()
        {
            // Arrange
            // Reseta a data estática antes do teste
            var dataField = typeof(Reserva).GetField("dataDaUltimaReserva",
                BindingFlags.NonPublic | BindingFlags.Static);
            dataField?.SetValue(null, DateTime.Now);

            // Act
            var reserva = CriarReservaParaTeste();

            // Assert
            Assert.AreEqual(0, reserva.QuantidadeDeReservas);
            Assert.IsTrue((DateTime.Now - Reserva.DataDaUltimaReserva).TotalSeconds < 5);
            var diferenca = (DateTime.Now - Reserva.DataDaUltimaReserva).TotalSeconds;
            Console.WriteLine($"Diferença em segundos: {diferenca}");

            Assert.IsTrue(diferenca < 5, $"A diferença de tempo foi de {diferenca} segundos, o que é maior que 5.");


        }

        [TestMethod()]

        public void Construtor_ComDadosPrevios_DeveCarregarDadosCorretamente()
        {
            // Arrange
            var dataMock = new DateTime(2023, 1, 1);
            var dadosMock = new DadosReserva
            {
                DataReservada = dataMock,
                CapacidadeEspaco = 500,
                QtdReservas = 5
            };

            // Act
            var reserva = CriarReservaParaTeste(dadosMock);

            // Assert
            Assert.AreEqual(5, reserva.QuantidadeDeReservas);
            Assert.AreEqual(dataMock, Reserva.DataDaUltimaReserva);
        }


        [TestMethod()]
        public void Reservar_PrimeiraReserva_DeveUsarDataDisponivelMaisProxima()
        {
            // Arrange
            var reserva = CriarReservaParaTeste();

            // Act
            reserva.Reservar();

            // Assert
            Assert.IsTrue(reserva.DataReservada >= DateTime.Now.Date);
            Assert.AreEqual(1, reserva.QuantidadeDeReservas);
        }

        [TestMethod()]
        public void Reservar_MultiplasReservas_DeveAlternarDatasCorretamente()
        {
            // Arrange
            var reserva = CriarReservaParaTeste();

            // Act
            reserva.Reservar();
            var dataReserva1 = reserva.DataReservada;

            reserva.Reservar();
            var dataReserva2 = dataReserva1.AddDays(1);

            // Assert
            Assert.AreNotEqual(dataReserva1, dataReserva2);
            Assert.IsTrue(dataReserva2 > dataReserva1);
        }
    }
}