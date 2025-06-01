using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Trabalho_POO;

namespace Trabalho_POO_Tests
{
    [TestClass]
    public class CasamentoLuxoTests
    {
        [TestMethod]
        public void SetItens_ShouldReturnCorrectDictionary()
        {
            var casamentoLuxo = new CasamentoLuxo(new List<int>());

            var itens = casamentoLuxo.DadosUtensilios;

            Assert.AreEqual(4, itens.Count, "O número de itens no dicionário está incorreto.");
            Assert.IsTrue(itens.ContainsKey("Itens de mesa"));
            Assert.IsTrue(itens.ContainsKey("Decoração"));
            Assert.IsTrue(itens.ContainsKey("Bolo"));
            Assert.IsTrue(itens.ContainsKey("Música"));

            Assert.AreEqual(75, itens["Itens de mesa"]);
            Assert.AreEqual(75, itens["Decoração"]);
            Assert.AreEqual(15, itens["Bolo"]);
            Assert.AreEqual(25, itens["Música"]);
        }

    }
}
