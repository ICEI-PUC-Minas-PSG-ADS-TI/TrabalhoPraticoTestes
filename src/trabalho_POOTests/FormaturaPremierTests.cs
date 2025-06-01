using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trabalho_POO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO.Tests
{
    [TestClass()]
    public class FormaturaPremierTests
    {
        [TestMethod()]
        public void FormaturaPremier_Constructor_DeveInicializarPropriedadesCorretamente()
        {
            var quantidadesBebidas = new List<int> { 50, 25, 10 };

            var formatura = new FormaturaPremier(quantidadesBebidas);

            Assert.AreEqual("Formatura premier", formatura.TipoFesta, "O tipo da festa está incorreto.");

            Assert.IsInstanceOfType(formatura.Comida, typeof(ComidaPremier), "A propriedade Comida não é do tipo ComidaPremier.");
            Assert.IsInstanceOfType(formatura.Bebida, typeof(BebidaPremier), "A propriedade Bebida não é do tipo BebidaPremier.");

            var itens = formatura.DadosUtensilios;
            Assert.IsNotNull(itens, "O dicionário de itens não foi inicializado.");
            Assert.AreEqual(3, itens.Count, "O dicionário deveria conter 3 itens.");

            Assert.IsTrue(itens.ContainsKey("Itens de mesa"), "Não foi encontrado o item 'Itens de mesa'.");
            Assert.AreEqual(100.0, itens["Itens de mesa"], "O preço para 'Itens de mesa' está incorreto.");

            Assert.IsTrue(itens.ContainsKey("Decoração"), "Não foi encontrado o item 'Decoração'.");
            Assert.AreEqual(100.0, itens["Decoração"], "O preço para 'Decoração' está incorreto.");

            Assert.IsTrue(itens.ContainsKey("Música"), "Não foi encontrado o item 'Música'.");
            Assert.AreEqual(30.0, itens["Música"], "O preço para 'Música' está incorreto.");
        }
    }
}