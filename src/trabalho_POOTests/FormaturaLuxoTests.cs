using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trabalho_POO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO.Tests
{
    [TestClass()] // Atributo adicionado para a classe ser reconhecida como de teste
    public class FormaturaLuxoTests
    {
        [TestMethod()]
        public void FormaturaLuxo_Constructor_DeveInicializarPropriedadesCorretamente()
        {

            var quantidadesBebidas = new List<int> { 50, 100, 30 };

            var formatura = new FormaturaLuxo(quantidadesBebidas);
            Assert.AreEqual("Formatura luxo", formatura.TipoFesta, "O tipo da festa está incorreto.");
            Assert.IsInstanceOfType(formatura.Comida, typeof(ComidaLuxo), "A propriedade Comida não é do tipo ComidaLuxo.");
            Assert.IsInstanceOfType(formatura.Bebida, typeof(BebidaLuxo), "A propriedade Bebida não é do tipo BebidaLuxo.");

            var itens = formatura.DadosUtensilios;
            Assert.IsNotNull(itens, "O dicionário de itens não foi inicializado.");
            Assert.AreEqual(3, itens.Count, "O dicionário deveria conter 3 itens.");

            Assert.IsTrue(itens.ContainsKey("Itens de mesa"), "Não foi encontrado o item 'Itens de mesa'.");
            Assert.AreEqual(75.0, itens["Itens de mesa"], "O preço para 'Itens de mesa' está incorreto.");

            Assert.IsTrue(itens.ContainsKey("Decoração"), "Não foi encontrado o item 'Decoração'.");
            Assert.AreEqual(75.0, itens["Decoração"], "O preço para 'Decoração' está incorreto.");

            Assert.IsTrue(itens.ContainsKey("Música"), "Não foi encontrado o item 'Música'.");
            Assert.AreEqual(25.0, itens["Música"], "O preço para 'Música' está incorreto.");
        }
    }
}