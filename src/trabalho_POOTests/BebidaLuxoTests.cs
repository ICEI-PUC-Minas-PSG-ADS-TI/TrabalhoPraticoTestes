using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace Trabalho_POO.Tests 
{
    [TestClass()]
    public class BebidaLuxoTests
    {
        [TestMethod()]
        public void Constructor_BebidaLuxo_PopulaListaBebidasCorretamente()
        {
            var quantidades = new List<int> { 1, 2, 3, 4, 5, 6, 7 }; 

            var bebidaLuxo = new BebidaLuxo(quantidades);

            Assert.IsNotNull(bebidaLuxo.Lista_Bebidas, "Lista_Bebidas não deveria ser nula.");
            Assert.AreEqual(7, bebidaLuxo.Lista_Bebidas.Count, "Número incorreto de tipos de bebida na lista.");

            Assert.IsTrue(bebidaLuxo.Lista_Bebidas.ContainsKey("Água sem gás (1,5L)"));
            Assert.AreEqual(5, bebidaLuxo.Lista_Bebidas["Água sem gás (1,5L)"]);

            Assert.IsTrue(bebidaLuxo.Lista_Bebidas.ContainsKey("Suco (1L)"));
            Assert.AreEqual(7, bebidaLuxo.Lista_Bebidas["Suco (1L)"]);

            Assert.IsTrue(bebidaLuxo.Lista_Bebidas.ContainsKey("Refrigerante (2L)"));
            Assert.AreEqual(8, bebidaLuxo.Lista_Bebidas["Refrigerante (2L)"]);

            Assert.IsTrue(bebidaLuxo.Lista_Bebidas.ContainsKey("Cerveja Comum (600ml)"));
            Assert.AreEqual(20, bebidaLuxo.Lista_Bebidas["Cerveja Comum (600ml)"]);

            Assert.IsTrue(bebidaLuxo.Lista_Bebidas.ContainsKey("Cerveja Artesanal (600ml)"));
            Assert.AreEqual(30, bebidaLuxo.Lista_Bebidas["Cerveja Artesanal (600ml)"]);

            Assert.IsTrue(bebidaLuxo.Lista_Bebidas.ContainsKey("Espumante nacional (750ml)"));
            Assert.AreEqual(80, bebidaLuxo.Lista_Bebidas["Espumante nacional (750ml)"]);

            Assert.IsTrue(bebidaLuxo.Lista_Bebidas.ContainsKey("Espumante importado (750ml)"));
            Assert.AreEqual(140, bebidaLuxo.Lista_Bebidas["Espumante importado (750ml)"]);
        }

        [TestMethod()]
        public void Constructor_BebidaLuxo_DefineQuantidadesCorretamente()
        {
            var quantidadesEsperadas = new List<int> { 10, 0, 5, 1, 8, 2, 4 };

            var bebidaLuxo = new BebidaLuxo(quantidadesEsperadas);

            Assert.IsNotNull(bebidaLuxo.Qtd_Bebida, "Qtd_Bebida não deveria ser nula.");
            Assert.AreEqual(quantidadesEsperadas.Count, bebidaLuxo.Qtd_Bebida.Count, "Número de quantidades diferente do esperado.");

            for (int i = 0; i < quantidadesEsperadas.Count; i++)
            {
                Assert.AreEqual(quantidadesEsperadas[i], bebidaLuxo.Qtd_Bebida[i], $"Quantidade incorreta para a bebida no índice {i}.");
            }
        }

        [TestMethod()]
        public void Constructor_BebidaLuxo_ListaBebidasNaoEhModificadaPorQuantidadesMenores()
        {
            var quantidadesCurtas = new List<int> { 1, 1 };
            int numeroDeTiposDeBebidaDefinidos = 7;

            var bebidaLuxo = new BebidaLuxo(quantidadesCurtas);

            Assert.IsNotNull(bebidaLuxo.Lista_Bebidas);
            Assert.AreEqual(numeroDeTiposDeBebidaDefinidos, bebidaLuxo.Lista_Bebidas.Count, "Lista_Bebidas deveria ter todos os tipos definidos por SetBebidas.");

            Assert.IsNotNull(bebidaLuxo.Qtd_Bebida);
            Assert.AreEqual(quantidadesCurtas.Count, bebidaLuxo.Qtd_Bebida.Count, "Qtd_Bebida deveria ter o tamanho da lista de quantidades fornecida.");
            Assert.AreEqual(quantidadesCurtas[0], bebidaLuxo.Qtd_Bebida[0]);
            Assert.AreEqual(quantidadesCurtas[1], bebidaLuxo.Qtd_Bebida[1]);
        }

        [TestMethod()]
        public void Constructor_BebidaLuxo_QuantidadesMaisLongasQueTiposDeBebida()
        {
            var quantidadesLongas = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int numeroDeTiposDeBebidaDefinidos = 7; 

            var bebidaLuxo = new BebidaLuxo(quantidadesLongas);

            Assert.IsNotNull(bebidaLuxo.Lista_Bebidas);
            Assert.AreEqual(numeroDeTiposDeBebidaDefinidos, bebidaLuxo.Lista_Bebidas.Count, "Lista_Bebidas deveria ter todos os tipos definidos por SetBebidas.");

            Assert.IsNotNull(bebidaLuxo.Qtd_Bebida);
            Assert.AreEqual(quantidadesLongas.Count, bebidaLuxo.Qtd_Bebida.Count, "Qtd_Bebida deveria ter o tamanho da lista de quantidades fornecida.");
            for (int i = 0; i < quantidadesLongas.Count; i++)
            {
                Assert.AreEqual(quantidadesLongas[i], bebidaLuxo.Qtd_Bebida[i]);
            }
        }

        [TestMethod()]
        public void Constructor_BebidaLuxo_ComListaQuantidadesVazia()
        {
            var quantidadesVazias = new List<int>();
            int numeroDeTiposDeBebidaDefinidos = 7; 

            var bebidaLuxo = new BebidaLuxo(quantidadesVazias);

            Assert.IsNotNull(bebidaLuxo.Lista_Bebidas);
            Assert.AreEqual(numeroDeTiposDeBebidaDefinidos, bebidaLuxo.Lista_Bebidas.Count);

            Assert.IsNotNull(bebidaLuxo.Qtd_Bebida);
            Assert.AreEqual(0, bebidaLuxo.Qtd_Bebida.Count);
        }
    }
}