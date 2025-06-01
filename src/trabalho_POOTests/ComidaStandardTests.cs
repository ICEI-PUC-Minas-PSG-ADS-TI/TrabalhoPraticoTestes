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
    public class ComidaStandardTests
    {
        [TestMethod()] 
        public void ComidaStandard_Constructor_DeveInicializarItensComida()
        {
            var comidaStandard = new ComidaStandard();
            Assert.IsNotNull(comidaStandard.Itens_Comida);
            Assert.AreEqual(1, comidaStandard.Itens_Comida.Count);
        }

        [TestMethod()]
        public void ComidaStandard_ItensComida_DeveConterDadosCorretos()
        {
            var comidaStandard = new ComidaStandard();
            var chaveEsperada = "Coxinha, Kibe, Empadinha, Pão de queijo";
            var valorEsperado = 40.0;
            Assert.IsTrue(comidaStandard.Itens_Comida.ContainsKey(chaveEsperada), "A chave com a descrição dos itens não foi encontrada.");

            Assert.AreEqual(valorEsperado, comidaStandard.Itens_Comida[chaveEsperada]);
        }
    }
}