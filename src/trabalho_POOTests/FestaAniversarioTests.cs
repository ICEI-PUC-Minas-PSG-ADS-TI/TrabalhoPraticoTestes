using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Trabalho_POO;

namespace Trabalho_POO_Tests
{
    [TestClass]
    public class FestaAniversarioTests
    {
        [TestMethod]
        public void SetItens_ShouldReturnCorrectDictionary()
        {
            var festaAniversario = new FestaAniversario(new List<int>());

            var itens = festaAniversario.DadosUtensilios;

            Assert.AreEqual(4, itens.Count, "O número de itens no dicionário está incorreto.");
            Assert.IsTrue(itens.ContainsKey("Itens de mesa"), "O item 'Itens de mesa' não foi encontrado.");
            Assert.IsTrue(itens.ContainsKey("Decoração"), "O item 'Decoração' não foi encontrado.");
            Assert.IsTrue(itens.ContainsKey("Bolo"), "O item 'Bolo' não foi encontrado.");
            Assert.IsTrue(itens.ContainsKey("Música"), "O item 'Música' não foi encontrado.");

            Assert.AreEqual(50, itens["Itens de mesa"], "O valor do item 'Itens de mesa' está incorreto.");
            Assert.AreEqual(50, itens["Decoração"], "O valor do item 'Decoração' está incorreto.");
            Assert.AreEqual(10, itens["Bolo"], "O valor do item 'Bolo' está incorreto.");
            Assert.AreEqual(20, itens["Música"], "O valor do item 'Música' está incorreto.");
        }

        [TestMethod]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            var quantidadesBebidas = new List<int> { 5, 10, 15, 20, 25 };
            var festaAniversario = new FestaAniversario(quantidadesBebidas);

            var tipoFesta = festaAniversario.TipoFesta;
            var dadosUtensilios = festaAniversario.DadosUtensilios;
            var comida = festaAniversario.Comida;
            var bebida = festaAniversario.Bebida;

            Assert.AreEqual("Festa de Aniversário", tipoFesta, "O tipo da festa não foi inicializado corretamente.");

            Assert.IsNotNull(dadosUtensilios, "A propriedade DadosUtensilios não foi inicializada.");
            Assert.AreEqual(4, dadosUtensilios.Count, "O número de itens em DadosUtensilios está incorreto.");

            Assert.IsNotNull(comida, "A propriedade Comida não foi inicializada.");
            Assert.IsInstanceOfType(comida, typeof(ComidaStandard), "A propriedade Comida não é do tipo esperado.");

            Assert.IsNotNull(bebida, "A propriedade Bebida não foi inicializada.");
            Assert.IsInstanceOfType(bebida, typeof(BebidaStandard), "A propriedade Bebida não é do tipo esperado.");
            Assert.AreEqual(quantidadesBebidas.Count, bebida.Qtd_Bebida.Count, "A quantidade de bebidas não corresponde à lista fornecida.");
            CollectionAssert.AreEqual(quantidadesBebidas, bebida.Qtd_Bebida, "Os valores da lista de quantidades de bebidas não correspondem.");
        }
    }
}