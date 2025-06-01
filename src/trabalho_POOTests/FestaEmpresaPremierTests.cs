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
    public class FestaEmpresaPremierTests
    {
        [TestMethod()]
        public void FestaEmpresaPremier_Constructor_DeveInicializarPropriedadesCorretamente()
        {
            var quantidadesBebidas = new List<int> { 10, 20, 5 };

            var festa = new FestaEmpresaPremier(quantidadesBebidas);
            Assert.AreEqual("Festa de empresa premier", festa.TipoFesta, "O tipo da festa não foi definido corretamente.");

            Assert.IsInstanceOfType(festa.Comida, typeof(ComidaPremier), "O objeto Comida não é do tipo ComidaPremier.");
            Assert.IsInstanceOfType(festa.Bebida, typeof(BebidaPremier), "O objeto Bebida não é do tipo BebidaPremier.");

            Assert.IsNotNull(festa.DadosUtensilios, "O dicionário de utensílios não foi inicializado.");
            Assert.AreEqual(1, festa.DadosUtensilios.Count, "O dicionário de utensílios deveria conter apenas 1 item.");
            Assert.IsTrue(festa.DadosUtensilios.ContainsKey("Música"), "O dicionário não contém o item 'Música'.");
            Assert.AreEqual(30.0, festa.DadosUtensilios["Música"], "O preço do item 'Música' está incorreto.");
        }
    }
}