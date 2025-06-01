using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trabalho_POO.Tests
{
  
    [TestClass()]
    public class FestaEmpresaLuxoTests
    {
        [TestMethod()]
        public void Constructor_FestaEmpresaLuxo_InitializesPropertiesCorrectly()
        {

            var quantidadesBebidasInput = new List<int> { 5, 5, 5, 10, 8, 2, 1 }; 

            var festa = new FestaEmpresaLuxo(quantidadesBebidasInput);

            Assert.IsNotNull(festa.TipoFesta, "TipoFesta não deveria ser nulo.");
            Assert.AreEqual("Festa de empresa luxo", festa.TipoFesta, "TipoFesta incorreto.");

            Assert.IsNotNull(festa.DadosUtensilios, "DadosUtensilios não deveria ser nulo.");
            Assert.AreEqual(1, festa.DadosUtensilios.Count, "Número incorreto de utensílios.");
            Assert.IsTrue(festa.DadosUtensilios.ContainsKey("Música"), "Utensílio 'Música' não encontrado.");
            Assert.AreEqual(25, festa.DadosUtensilios["Música"], "Preço do utensílio 'Música' incorreto.");

            Assert.IsNotNull(festa.Comida, "Comida não deveria ser nula.");
            Assert.IsInstanceOfType(festa.Comida, typeof(ComidaLuxo), "Tipo de Comida incorreto.");
            var comidaLuxo = festa.Comida as ComidaLuxo;
            Assert.IsNotNull(comidaLuxo.Itens_Comida, "Itens_Comida em ComidaLuxo não deveria ser nulo.");
            Assert.AreEqual(1, comidaLuxo.Itens_Comida.Count, "ComidaLuxo deveria ter 1 item definido.");
            Assert.IsTrue(comidaLuxo.Itens_Comida.ContainsKey("Croquete carne seca, Barquetes legumes, Empadinha gourmet, Cestinha bacalhau"), "Item chave da ComidaLuxo não encontrado.");
            Assert.AreEqual(48, comidaLuxo.Itens_Comida["Croquete carne seca, Barquetes legumes, Empadinha gourmet, Cestinha bacalhau"], "Preço do item da ComidaLuxo incorreto.");

            Assert.IsNotNull(festa.Bebida, "Bebida não deveria ser nula.");
            Assert.IsInstanceOfType(festa.Bebida, typeof(BebidaLuxo), "Tipo de Bebida incorreto.");
            var bebidaLuxo = festa.Bebida as BebidaLuxo;

            Assert.IsNotNull(bebidaLuxo.Lista_Bebidas, "Lista_Bebidas em BebidaLuxo não deveria ser nula.");
            Assert.AreEqual(7, bebidaLuxo.Lista_Bebidas.Count, "BebidaLuxo deveria ter 7 tipos de bebida definidos.");
            Assert.IsTrue(bebidaLuxo.Lista_Bebidas.ContainsKey("Espumante importado (750ml)"), "Bebida chave 'Espumante importado (750ml)' não encontrada em BebidaLuxo.");
            Assert.AreEqual(140, bebidaLuxo.Lista_Bebidas["Espumante importado (750ml)"], "Preço do 'Espumante importado (750ml)' incorreto em BebidaLuxo.");

            Assert.IsNotNull(bebidaLuxo.Qtd_Bebida, "Qtd_Bebida em BebidaLuxo não deveria ser nula.");
            CollectionAssert.AreEqual(quantidadesBebidasInput, bebidaLuxo.Qtd_Bebida, "Quantidades de bebida não correspondem às fornecidas.");
        }

        [TestMethod()]
        public void SetItens_FestaEmpresaLuxo_ReturnsCorrectDictionary()
        {
            var festaHelper = new FestaEmpresaLuxo_TestHelper(new List<int>());

            Dictionary<string, double> itens = festaHelper.ChamarSetItens();

            Assert.IsNotNull(itens);
            Assert.AreEqual(1, itens.Count);
            Assert.IsTrue(itens.ContainsKey("Música"));
            Assert.AreEqual(25, itens["Música"]);
        }

        private class FestaEmpresaLuxo_TestHelper : FestaEmpresaLuxo
        {
            public FestaEmpresaLuxo_TestHelper(List<int> quantidadesBebidas) : base(quantidadesBebidas) { }

            public Dictionary<string, double> ChamarSetItens()
            {
                return base.SetItens(); 
            }
        }
    }
}