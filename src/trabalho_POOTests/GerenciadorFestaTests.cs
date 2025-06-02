using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Trabalho_POO;

namespace Trabalho_POOTests
{
    [TestClass]
    public class GerenciadorFestaTests
    {
        private Mock<Reserva> _mockReserva;

        [TestInitialize]
        public void Setup()
        {
            _mockReserva = new Mock<Reserva>();
            _mockReserva.Setup(r => r.DataReservada).Returns(DateTime.Now.AddDays(30));
            _mockReserva.Setup(r => r.Espaco.CapacidadeDoEspaco).Returns(100);
            _mockReserva.Setup(r => r.Espaco.Preco).Returns(5000);
            _mockReserva.Setup(r => r.QuantidadeDeReservas).Returns(1);
        }

        [TestMethod]
        public void GerenciadorFesta_InstanciarReserva_Quantidade50_DeveRetornarReservaCinquenta()
        {
            int quantidadeDeParticipantes = 50;

            var gerenciador = new GerenciadorFesta(quantidadeDeParticipantes, reserva: _mockReserva.Object);

            Assert.IsNotNull(gerenciador.Reservas);
            Assert.AreEqual(100, gerenciador.Reservas.Espaco.CapacidadeDoEspaco); // Capacidade simulada no mock
        }

        [TestMethod]
        public void GerarResumo_DeveRetornarResumoCorreto()
        {
            int quantidadeDeParticipantes = 100;
            string tipoDeFesta = "FestaEmpresaLuxo";
            List<int> quantidadesBebidas = new List<int> { 15, 25, 35 };

            var gerenciador = new GerenciadorFesta(quantidadeDeParticipantes, tipoDeFesta, quantidadesBebidas, _mockReserva.Object);
            var resumo = gerenciador.GerarResumo();

            Assert.IsNotNull(resumo);
            Assert.AreEqual("Festa de empresa luxo", resumo.TipoFesta);
            Assert.AreEqual(100, resumo.CapacidadeEspaco);
            Assert.IsTrue(resumo.ValorTotalFesta > 0);
        }

        [TestMethod]
        public void GerarResumoEventoLivre_DeveRetornarResumoCorreto()
        {
            int quantidadeDeParticipantes = 30;

            var gerenciador = new GerenciadorFesta(quantidadeDeParticipantes, reserva: _mockReserva.Object);
            var resumo = gerenciador.GerarResumoEventoLivre();

            Assert.IsNotNull(resumo);

            Assert.AreEqual("Evento livre", resumo.TipoFesta);
            Assert.AreEqual(100, resumo.CapacidadeEspaco);
            Assert.IsTrue(resumo.ValorTotalFesta > 0);
        }

        [TestMethod]
        public void GerarDados_DeveRetornarDadosCorretos()
        {
            int quantidadeDeParticipantes = 150;
            string tipoDeFesta = "FormaturaStandard";
            List<int> quantidadesBebidas = new List<int> { 5, 10, 15 };

            var gerenciador = new GerenciadorFesta(quantidadeDeParticipantes, tipoDeFesta, quantidadesBebidas, _mockReserva.Object);
            var dados = gerenciador.GerarDados();

            Assert.IsNotNull(dados);
            Assert.AreEqual(100, dados.CapacidadeEspaco);
            Assert.AreEqual(1, dados.QtdReservas);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GerenciadorFesta_InstanciarEvento_TipoInvalido_DeveLancarExcecao()
        {
            int quantidadeDeParticipantes = 100;
            string tipoDeFesta = "TipoInvalido";
            List<int> quantidadesBebidas = new List<int> { 10, 20, 30 };

            var gerenciador = new GerenciadorFesta(quantidadeDeParticipantes, tipoDeFesta, quantidadesBebidas, _mockReserva.Object);

        }
    }
}