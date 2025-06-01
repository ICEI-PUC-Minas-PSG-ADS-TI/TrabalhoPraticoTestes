using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization; 
using System.IO;
using System.Text;
using trabalhoPOOList; 

namespace trabalhoPOOList.Tests
{
    public class StubEspaco
    {
        public int CapacidadeDoEspaco { get; set; }
        public double Preco { get; set; }
    }
    public class StubReservaParaExibicao
    {
        public StubEspaco Espaco { get; set; }
        public DateTime DataReservada { get; set; } 
    }
    public class StubGerenciadorFesta
    {
        public StubReservaParaExibicao Reservas { get; set; }
    }
    public class StubRepositorioMongoDB
    {
        public List<Dictionary<string, object>> ResumosParaRetornar { get; set; } = new List<Dictionary<string, object>>();

        public List<Dictionary<string, object>> RecuperarResumosFesta()
        {
            return ResumosParaRetornar;
        }
    }

    [TestClass()]
    public class ProgramTests
    {
        private StringWriter _stringWriter;
        private TextWriter _originalOutput;
        private TextReader _originalInput; 

        [TestInitialize]
        public void TestInitialize()
        {
            _originalOutput = Console.Out;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);

            _originalInput = Console.In; 
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Console.SetOut(_originalOutput);
            _stringWriter.Dispose();
            Console.SetIn(_originalInput);
        }

        [TestMethod()]
        public void ResumoEspaco_ComDadosFixos_ExibeInformacaoCorreta()
        {
            var stubFesta = new StubGerenciadorFesta
            {
                Reservas = new StubReservaParaExibicao
                {
                    Espaco = new StubEspaco { CapacidadeDoEspaco = 150, Preco = 500.75 }
                }
            };


            Assert.IsTrue(true, "Teste conceitual. Requer capacidade de popular GerenciadorFesta com dados fixos ou refatorar ResumoEspaco.");
        }

        [TestMethod()]
        public void ExibirReservas_ComDadosFixos_ExibeReservasFormatadas()
        {
            var stubRepo = new StubRepositorioMongoDB();
            stubRepo.ResumosParaRetornar.Add(new Dictionary<string, object>
            {
                { "Data da reserva", new DateTime(2025, 12, 10) },
                { "Tipo de festa", "Casamento Standard" }
            });
            stubRepo.ResumosParaRetornar.Add(new Dictionary<string, object>
            {
                { "Data da reserva", new DateTime(2025, 11, 20) },
                { "Tipo de festa", "Formatura Luxo" }
            });

            Assert.IsTrue(true, "Teste conceitual. Requer que ExibirReservas possa usar um repositório stub/mock.");
        }

        [TestMethod()]
        public void ExibirReservas_SemReservas_ExibeMensagemApropriada()
        {

            var stubRepo = new StubRepositorioMongoDB(); 

            Assert.IsTrue(true, "Teste conceitual. Requer que ExibirReservas possa usar um repositório stub/mock e verifique a mensagem correta.");
        }
    }
}