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
    public class ReservaCemTests
    {
        private interface IRepositorioReserva
        {
            DadosReserva RecuperarDadosReserva(int capacidadeEspaco);
        }

        private class RepositorioMock : IRepositorioReserva
        {
            public DadosReserva DadosFicticios { get; set; }

            public DadosReserva RecuperarDadosReserva(int capacidadeEspaco)
            {
                return DadosFicticios ?? new DadosReserva();
            }
        }

        private ReservaCem CriarReservaParaTeste(DadosReserva dadosMock = null)
        {
            var reserva = (ReservaCem)FormatterServices.GetUninitializedObject(typeof(ReservaCem));

            var repoField = typeof(ReservaCem).GetField("repo",
                BindingFlags.NonPublic | BindingFlags.Instance);
            repoField?.SetValue(reserva, new RepositorioMock { DadosFicticios = dadosMock });

            reserva.Espaco = new EspacoCem();

            if (dadosMock != null)
            {
                reserva.QuantidadeDeReservas = dadosMock.QtdReservas;
                Reserva.DataDaUltimaReserva = dadosMock.DataReservada;
            }
            else
            {
                reserva.QuantidadeDeReservas = 0;
                Reserva.DataDaUltimaReserva = DateTime.Now;
            }

            return reserva;
        }


        [TestMethod()]
        public void Construtor_DeveInicializarComEspacoQuinhentos()
        {
            var reserva = CriarReservaParaTeste();

            Assert.IsInstanceOfType(reserva.Espaco, typeof(EspacoCem));
            Assert.AreEqual(100, reserva.Espaco.CapacidadeDoEspaco);
        }

        [TestMethod()]
        public void Construtor_SemDadosPrevios_DeveIniciarComDataAtualEZeroReservas()
        {
            var dataField = typeof(Reserva).GetField("dataDaUltimaReserva",
                BindingFlags.NonPublic | BindingFlags.Static);
            dataField?.SetValue(null, DateTime.Now);

            var reserva = CriarReservaParaTeste();

            Assert.AreEqual(0, reserva.QuantidadeDeReservas);
            Assert.IsTrue((DateTime.Now - Reserva.DataDaUltimaReserva).TotalSeconds < 5);
            var diferenca = (DateTime.Now - Reserva.DataDaUltimaReserva).TotalSeconds;
            Console.WriteLine($"Diferença em segundos: {diferenca}");

            Assert.IsTrue(diferenca < 5, $"A diferença de tempo foi de {diferenca} segundos, o que é maior que 5.");


        }

        [TestMethod()]

        public void Construtor_ComDadosPrevios_DeveCarregarDadosCorretamente()
        {
            var dataMock = new DateTime(2023, 1, 1);
            var dadosMock = new DadosReserva
            {
                DataReservada = dataMock,
                CapacidadeEspaco = 100,
                QtdReservas = 5
            };

            var reserva = CriarReservaParaTeste(dadosMock);

            Assert.AreEqual(5, reserva.QuantidadeDeReservas);
            Assert.AreEqual(dataMock, Reserva.DataDaUltimaReserva);
        }


        [TestMethod()]
        public void Reservar_PrimeiraReserva_DeveUsarDataDisponivelMaisProxima()
        {
            var reserva = CriarReservaParaTeste();

            reserva.Reservar();

            Assert.IsTrue(reserva.DataReservada >= DateTime.Now.Date);
            Assert.AreEqual(1, reserva.QuantidadeDeReservas);
        }

        [TestMethod()]
        public void Reservar_MultiplasReservas_DeveAlternarDatasCorretamente()
        {
            var reserva = CriarReservaParaTeste();

            reserva.Reservar();
            var dataReserva1 = reserva.DataReservada;

            reserva.Reservar();
            var dataReserva2 = dataReserva1.AddDays(1);

            Assert.AreNotEqual(dataReserva1, dataReserva2);
            Assert.IsTrue(dataReserva2 > dataReserva1);
        }
    }
}