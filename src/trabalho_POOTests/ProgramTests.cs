using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Moq;
using Trabalho_POO;

namespace trabalhoPOOList.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private StringWriter _stringWriter;
        private StringReader _stringReader;
        private TextWriter _originalConsoleOut;
        private TextReader _originalConsoleIn;

        private interface IRepositorioReserva
        {
            DadosReserva RecuperarDadosReserva(int capacidadeEspaco);
        }

        private class RepositorioMockUser : IRepositorioReserva
        {
            public DadosReserva DadosFicticios { get; set; }

            public DadosReserva RecuperarDadosReserva(int capacidadeEspaco)
            {
                return DadosFicticios ?? new DadosReserva();
            }
        }
        public class TestableRepositorioMongoDBForExibir : RepositorioMongoDB
        {
            public List<BsonDocument> MockResumos { get; set; } = new List<BsonDocument>();
            public bool GuardaDadosResumoCalled { get; private set; } = false;
            public bool GuardaDatasReservadasCalled { get; private set; } = false;

            public List<BsonDocument> RecuperarResumosFesta()
            {
                return MockResumos;
            }

            public void GuardaDadosResumo(BsonDocument resumo)
            {
                GuardaDadosResumoCalled = true;
            }

            public void GuardaDatasReservadas(BsonDocument data)
            {
                GuardaDatasReservadasCalled = true;
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _originalConsoleOut = Console.Out;
            _originalConsoleIn = Console.In;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);

            Program.IsTestingEnvironment = true;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Console.SetOut(_originalConsoleOut);
            Console.SetIn(_originalConsoleIn);
            _stringWriter.Dispose();
            _stringReader?.Dispose();

            Program.IsTestingEnvironment = false;
        }

        private void SimulateConsoleInput(string input)
        {
            _stringReader = new StringReader(input);
            Console.SetIn(_stringReader);
        }

        private string GetConsoleOutput()
        {
            return _stringWriter.ToString();
        }

        [TestMethod]
        public void OpcaoMenuInicial_ValidInput1_Returns1()
        {
            SimulateConsoleInput("1\n");
            int result = Program.OpcaoMenuInicial();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void OpcaoMenuInicial_ValidInput0_Returns0()
        {
            SimulateConsoleInput("0\n");
            int result = Program.OpcaoMenuInicial();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void OpcaoMenuInicial_InvalidThenValidInput_ReturnsValid()
        {
            SimulateConsoleInput("x\n2\n");
            int result = Program.OpcaoMenuInicial();
            string output = GetConsoleOutput();
            Assert.IsTrue(output.Contains("Opção 0 inválida"));
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void OpcaoTipoCasamento_ValidInput1_Returns1()
        {
            SimulateConsoleInput("1\n");
            int result = Program.OpcaoTipoCasamento();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void OpcaoTipoCasamento_InvalidParseThenValidInput_ReturnsValid()
        {
            SimulateConsoleInput("abc\n1\n");
            int result = Program.OpcaoTipoCasamento();
            string output = GetConsoleOutput();
            Assert.IsTrue(output.Contains("Opção 0 inválida"));
            Assert.AreEqual(1, result);
        }


        [TestMethod]
        public void QuantidadeBebida_ValidInput_ReturnsQuantity()
        {
            SimulateConsoleInput("5\n");
            int result = Program.QuantidadeBebida();
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void QuantidadeBebida_InvalidThenValidInput_ReturnsValidQuantity()
        {
            SimulateConsoleInput("invalid\n10\n");
            int result = Program.QuantidadeBebida();
            string output = GetConsoleOutput();
            Assert.IsTrue(output.Contains("Quantidade 0 inválida"));
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void QuantidadeParticipantes_ValidInput_ReturnsQuantity()
        {
            SimulateConsoleInput("50\n");
            int result = Program.QuantidadeParticipantes();
            Assert.AreEqual(50, result);
        }

        [TestMethod]
        public void MenuAgendamento_Case0_DisplaysExitMessage()
        {
            string simulatedUserInput = "0\n";
            SimulateConsoleInput(simulatedUserInput);

            Program.MenuAgendamento();

            string output = GetConsoleOutput();

            Assert.IsTrue(output.Contains("Escolha o tipo de reserva desejado"), "Initial menu prompt missing.");
            Assert.IsTrue(output.Contains("Fim do programa"), "Exit message 'Fim do programa' missing.");
        }

    }

}