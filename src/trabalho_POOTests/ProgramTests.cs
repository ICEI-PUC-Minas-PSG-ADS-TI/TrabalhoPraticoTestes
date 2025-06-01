using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization; // For CultureInfo
using Trabalho_POO; // Assuming GerenciadorFesta and other classes are here
using trabalhoPOOList; // Your main program's namespace
using MongoDB.Bson; // For BsonDocument in ExibirReservas test

namespace trabalhoPOOList.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private StringWriter _stringWriter;
        private StringReader _stringReader;
        private TextWriter _originalConsoleOut;
        private TextReader _originalConsoleIn;

        // The user-added interface and its mock.
        // This is not currently used by Program.ExibirReservas tests as that method
        // expects a concrete RepositorioMongoDB instance.
        // This could be used if GerenciadorFesta or other parts of Program are refactored
        // to use IRepositorioReserva.
        // Ensure DadosReserva class is defined in your Trabalho_POO project or accessible.
        private interface IRepositorioReserva
        {
            DadosReserva RecuperarDadosReserva(int capacidadeEspaco);
        }

        private class RepositorioMockUser : IRepositorioReserva // Renamed to avoid conflict if you have another RepositorioMock
        {
            public DadosReserva DadosFicticios { get; set; }

            public DadosReserva RecuperarDadosReserva(int capacidadeEspaco)
            {
                // Assuming DadosReserva has a parameterless constructor or is a struct
                return DadosFicticios ?? new DadosReserva();
            }
        }

        //region Stub for RepositorioMongoDB
        // This stub is necessary to test Program.ExibirReservas because
        // Program.ExibirReservas(RepositorioMongoDB repo) takes a concrete class.
        // For this to work, the methods overridden here (e.g., RecuperarResumosFesta)
        // MUST be marked as 'virtual' in your actual 'RepositorioMongoDB' class.
        public class TestableRepositorioMongoDBForExibir : RepositorioMongoDB
        {
            public List<BsonDocument> MockResumos { get; set; } = new List<BsonDocument>();
            public bool GuardaDadosResumoCalled { get; private set; } = false;
            public bool GuardaDatasReservadasCalled { get; private set; } = false;

            // Ensure this method is VIRTUAL in your RepositorioMongoDB class
            public List<BsonDocument> RecuperarResumosFesta()
            {
                return MockResumos;
            }

            // Ensure this method is VIRTUAL in your RepositorioMongoDB class if needed for other tests
            public void GuardaDadosResumo(BsonDocument resumo)
            {
                GuardaDadosResumoCalled = true;
                // Optionally store 'resumo' for assertion
                // base.GuardaDadosResumo(resumo); // Call base if original logic is partially needed
            }

            // Ensure this method is VIRTUAL in your RepositorioMongoDB class if needed for other tests
            public void GuardaDatasReservadas(BsonDocument data)
            {
                GuardaDatasReservadasCalled = true;
                // Optionally store 'data' for assertion
                // base.GuardaDatasReservadas(data); // Call base if original logic is partially needed
            }
        }
        //endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _originalConsoleOut = Console.Out;
            _originalConsoleIn = Console.In;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Console.SetOut(_originalConsoleOut);
            Console.SetIn(_originalConsoleIn);
            _stringWriter.Dispose();
            _stringReader?.Dispose();
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
            SimulateConsoleInput("x\n2\n"); // Invalid input "x", then valid "2"
            int result = Program.OpcaoMenuInicial();
            string output = GetConsoleOutput();
            Assert.IsTrue(output.Contains("Opção 0 inválida")); // Assuming default int value is 0 for failed TryParse
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
            // This test addresses the case where input is not an integer.
            // The range validation `opcao < 0 && opcao > 3` in your SUT is logically flawed
            // (it will always be false) and doesn't cover the full menu range (0-11).
            // This test focuses on the TryParse part of the validation.
            SimulateConsoleInput("abc\n1\n"); // Non-integer, then valid
            int result = Program.OpcaoTipoCasamento();
            string output = GetConsoleOutput();
            Assert.IsTrue(output.Contains("Opção 0 inválida")); // Assuming default int value 0 for failed TryParse
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
            Assert.IsTrue(output.Contains("Quantidade 0 inválida")); // Default int is 0
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void QuantidadeParticipantes_ValidInput_ReturnsQuantity()
        {
            SimulateConsoleInput("50\n");
            int result = Program.QuantidadeParticipantes();
            Assert.AreEqual(50, result);
        }



    }

}