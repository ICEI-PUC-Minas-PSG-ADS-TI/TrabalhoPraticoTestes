using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Trabalho_POO;

namespace Trabalho_POO_Tests
{
    [TestClass]
    public class BebidaStandardTests
    {
        [TestMethod]
        public void SetBebidas_ShouldReturnCorrectDictionary()
        {
            var bebidaStandard = new BebidaStandard(new List<int>());

            var bebidas = bebidaStandard.Lista_Bebidas;

            Assert.AreEqual(5, bebidas.Count, "O número de bebidas no dicionário está incorreto.");
            Assert.IsTrue(bebidas.ContainsKey("Água sem gás (1,5L)"));
            Assert.IsTrue(bebidas.ContainsKey("Suco (1L)"));
            Assert.IsTrue(bebidas.ContainsKey("Refrigerante (2L)"));
            Assert.IsTrue(bebidas.ContainsKey("Cerveja Comum (600ml)"));
            Assert.IsTrue(bebidas.ContainsKey("Espumante nacional (750ml)"));

            Assert.AreEqual(5, bebidas["Água sem gás (1,5L)"]);
            Assert.AreEqual(7, bebidas["Suco (1L)"]);
            Assert.AreEqual(8, bebidas["Refrigerante (2L)"]);
            Assert.AreEqual(20, bebidas["Cerveja Comum (600ml)"]);
            Assert.AreEqual(80, bebidas["Espumante nacional (750ml)"]);
        }

    }
}