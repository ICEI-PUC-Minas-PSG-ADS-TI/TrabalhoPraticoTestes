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
    public class ComidaPremierTests
    {
        [TestMethod()]
        public void ComidaPremier_Constructor_DeveInicializarItensComida()
        {
            var comidaPremier = new ComidaPremier();
            Assert.IsNotNull(comidaPremier.Itens_Comida);
            Assert.AreEqual(1, comidaPremier.Itens_Comida.Count);
        }
        [TestMethod()]
        public void ComidaPremier_ItensComida_DeveConterDadosCorretos()
        {
            var comidaPremier = new ComidaPremier();
            var chaveEsperada = "Canapé, Tartine, Bruschetta, Espetinho caprese";
            var valorEsperado = 60.0;

            Assert.IsTrue(comidaPremier.Itens_Comida.ContainsKey(chaveEsperada), "A chave esperada não foi encontrada no dicionário.");

            Assert.AreEqual(valorEsperado, comidaPremier.Itens_Comida[chaveEsperada]);
        }
    }
}