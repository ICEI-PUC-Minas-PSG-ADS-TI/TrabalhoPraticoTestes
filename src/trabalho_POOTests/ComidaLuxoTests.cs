using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace Trabalho_POO.Tests 
{
    [TestClass()]
    public class ComidaLuxoTests
    {
        [TestMethod()]
        public void Constructor_ComidaLuxo_PopulaItensComidaCorretamente()
        {
            var comidaLuxo = new ComidaLuxo();

            Assert.IsNotNull(comidaLuxo.Itens_Comida, "Itens_Comida não deveria ser nulo.");
            Assert.AreEqual(1, comidaLuxo.Itens_Comida.Count, "Número incorreto de entradas de itens de comida.");

            string expectedKey = "Croquete carne seca, Barquetes legumes, Empadinha gourmet, Cestinha bacalhau";
            double expectedValue = 48;

            Assert.IsTrue(comidaLuxo.Itens_Comida.ContainsKey(expectedKey), $"A chave '{expectedKey}' não foi encontrada.");
            Assert.AreEqual(expectedValue, comidaLuxo.Itens_Comida[expectedKey], $"O valor para a chave '{expectedKey}' está incorreto.");
        }

        [TestMethod()]
        public void SetDadosSalgados_ComidaLuxo_RetornaDicionarioCorreto()
        {
            var comidaLuxo = new ComidaLuxo(); 

            string expectedKey = "Croquete carne seca, Barquetes legumes, Empadinha gourmet, Cestinha bacalhau";
            double expectedValue = 48;

            Assert.IsTrue(comidaLuxo.Itens_Comida.ContainsKey(expectedKey));
            Assert.AreEqual(expectedValue, comidaLuxo.Itens_Comida[expectedKey]);
            Assert.AreEqual(1, comidaLuxo.Itens_Comida.Count);
        }
    }
}