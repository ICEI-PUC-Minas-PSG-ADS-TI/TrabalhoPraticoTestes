using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Trabalho_POO;

namespace Trabalho_POO_Tests
{
    [TestClass]
    public class CasamentoPremierTests
    {
        [TestMethod]
        public void SetItens_ShouldReturnCorrectDictionary()
        {

            var casamentoPremier = new CasamentoPremier(new List<int>());

            var itens = casamentoPremier.DadosUtensilios;

            Assert.AreEqual(4, itens.Count, "O número de itens no dicionário está incorreto.");
            Assert.IsTrue(itens.ContainsKey("Itens de mesa"), "O item 'Itens de mesa' não foi encontrado.");
            Assert.IsTrue(itens.ContainsKey("Decoração"), "O item 'Decoração' não foi encontrado.");
            Assert.IsTrue(itens.ContainsKey("Bolo"), "O item 'Bolo' não foi encontrado.");
            Assert.IsTrue(itens.ContainsKey("Música"), "O item 'Música' não foi encontrado.");

            Assert.AreEqual(100, itens["Itens de mesa"], "O valor do item 'Itens de mesa' está incorreto.");
            Assert.AreEqual(100, itens["Decoração"], "O valor do item 'Decoração' está incorreto.");
            Assert.AreEqual(20, itens["Bolo"], "O valor do item 'Bolo' está incorreto.");
            Assert.AreEqual(30, itens["Música"], "O valor do item 'Música' está incorreto.");
        }
    }
}