using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trabalho_POO;
using System;
using System.Collections.Generic;
using Moq;
using trabalhoPOOList;

namespace Trabalho_POO.Tests
{
    [TestClass]
    public class CalculadoraEventosTests
    {
        private Reserva CriarReservaDeTestePadrao()
        {
            var mockEspaco = new Mock<Espaco>();
            mockEspaco.Setup(e => e.Preco).Returns(500.0);
            mockEspaco.Setup(e => e.CapacidadeDoEspaco).Returns(20);

            var mockReserva = new Mock<Reserva>();
            mockReserva.Setup(r => r.Espaco).Returns(mockEspaco.Object);

            return mockReserva.Object;
        }

        private Evento CriarEventoDeTestePadrao()
        {
            var mockBebida = new Mock<Bebida>();

            mockBebida.Setup(b => b.Lista_Bebidas)
                      .Returns(new Dictionary<string, double> { { "Refrigerante", 5.0 } });
            mockBebida.Setup(b => b.Qtd_Bebida)
                      .Returns(new List<int> { 10 });

            var mockComida = new Mock<Comida>();
            mockComida.Setup(c => c.Itens_Comida)
                      .Returns(new Dictionary<string, double> { { "Salgado", 3.0 } });

            var mockEvento = new Mock<Evento>();
            mockEvento.Setup(evt => evt.Bebida).Returns(mockBebida.Object);
            mockEvento.Setup(evt => evt.Comida).Returns(mockComida.Object);
            mockEvento.Setup(evt => evt.DadosUtensilios).Returns(new Dictionary<string, double> { { "Prato", 0.5 } });

            return mockEvento.Object;
        }

        private Evento CriarEventoDeTeste_Vazio()
        {
            var mockBebida = new Mock<Bebida>();
            mockBebida.Setup(b => b.Lista_Bebidas).Returns(new Dictionary<string, double>());
            mockBebida.Setup(b => b.Qtd_Bebida).Returns(new List<int>());

            var mockComida = new Mock<Comida>();
            mockComida.Setup(c => c.Itens_Comida).Returns(new Dictionary<string, double>());

            var mockEvento = new Mock<Evento>();
            mockEvento.Setup(evt => evt.Bebida).Returns(mockBebida.Object);
            mockEvento.Setup(evt => evt.Comida).Returns(mockComida.Object);
            mockEvento.Setup(evt => evt.DadosUtensilios).Returns(new Dictionary<string, double>());

            return mockEvento.Object;
        }


        [TestMethod()]
        public void CalcularBebidas_ComItensPadrao_DeveRetornarSomaCorreta()
        {
            var calculadora = new CalculadoraEventos();
            var festa = CriarEventoDeTestePadrao();
            double esperado = 50.0;

            double resultado = calculadora.CalcularBebidas(festa);

            Assert.AreEqual(esperado, resultado, "O cálculo de bebidas padrão falhou.");
        }

        [TestMethod()]
        public void CalcularBebidas_ComEventoVazio_DeveRetornarZero()
        {
            var calculadora = new CalculadoraEventos();
            var festa = CriarEventoDeTeste_Vazio();

            double resultado = calculadora.CalcularBebidas(festa);

            Assert.AreEqual(0.0, resultado, "O cálculo de bebidas com evento vazio deveria ser zero.");
        }

        [TestMethod()]
        public void CalcularComidas_ComItensPadrao_DeveRetornarSomaCorreta()
        {
            var calculadora = new CalculadoraEventos();
            var festa = CriarEventoDeTestePadrao();
            int quantidadeParticipantes = 10;
            double esperado = 30.0;

            double resultado = calculadora.CalcularComidas(festa, quantidadeParticipantes);

            Assert.AreEqual(esperado, resultado, "O cálculo de comidas padrão falhou.");
        }

        [TestMethod()]
        public void CalcularUtensilios_ComItensPadrao_DeveRetornarSomaCorreta()
        {
            var calculadora = new CalculadoraEventos();
            var festa = CriarEventoDeTestePadrao();
            var reserva = CriarReservaDeTestePadrao();
            double esperado = 10.0;

            double resultado = calculadora.CalcularUtensilios(festa, reserva);

            Assert.AreEqual(esperado, resultado, "O cálculo de utensílios padrão falhou.");
        }

        [TestMethod()]
        public void CalcularValorTotal_CenarioCompletoPadrao_DeveRetornarSomaDeTodosOsCustos()
        {
            var calculadora = new CalculadoraEventos();
            var festa = CriarEventoDeTestePadrao();
            var reserva = CriarReservaDeTestePadrao();
            int quantidadeParticipantes = 20;
            double esperado = 620.0;

            double resultado = calculadora.CalcularValorTotal(festa, quantidadeParticipantes, reserva);

            Assert.AreEqual(esperado, resultado, "O cálculo do valor total para o cenário padrão falhou.");
        }

 
    }
}