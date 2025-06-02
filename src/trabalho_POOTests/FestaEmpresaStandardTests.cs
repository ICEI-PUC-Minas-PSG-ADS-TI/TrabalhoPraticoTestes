using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Trabalho_POO;

namespace Trabalho_POOTests
{
    [TestClass]
    public class FestaEmpresaStandardTests
    {
        [TestMethod]
        public void FestaEmpresaStandard_DeveConfigurarTipoDeFestaCorretamente()
        {
            List<int> quantidadesBebidas = new List<int> { 10, 20, 30 };

            var festa = new FestaEmpresaStandard(quantidadesBebidas);

            Assert.AreEqual("Festa de empresa standard", festa.TipoFesta);
        }

        [TestMethod]
        public void FestaEmpresaStandard_DeveConfigurarUtensiliosCorretamente()
        {
            List<int> quantidadesBebidas = new List<int> { 10, 20, 30 };

            var festa = new FestaEmpresaStandard(quantidadesBebidas);
            var utensilios = festa.DadosUtensilios;

            Assert.IsNotNull(utensilios);
            Assert.IsTrue(utensilios.ContainsKey("Música"));
            Assert.AreEqual(20, utensilios["Música"]);
        }

        [TestMethod]
        public void FestaEmpresaStandard_DeveConfigurarComidaCorretamente()
        {
            List<int> quantidadesBebidas = new List<int> { 10, 20, 30 };

            var festa = new FestaEmpresaStandard(quantidadesBebidas);

            Assert.IsNotNull(festa.Comida);
            Assert.IsInstanceOfType(festa.Comida, typeof(ComidaStandard));
        }

        [TestMethod]
        public void FestaEmpresaStandard_DeveConfigurarBebidaCorretamente()
        {
            List<int> quantidadesBebidas = new List<int> { 10, 20, 30 };

            var festa = new FestaEmpresaStandard(quantidadesBebidas);

            Assert.IsNotNull(festa.Bebida);
            Assert.IsInstanceOfType(festa.Bebida, typeof(BebidaStandard));
        }

        [TestMethod]
        public void FestaEmpresaStandard_Bebida_DeveConterQuantidadesCorretas()
        {
            List<int> quantidadesBebidas = new List<int> { 10, 20, 30 };

            var festa = new FestaEmpresaStandard(quantidadesBebidas);
            var bebida = festa.Bebida as BebidaStandard;

            Assert.IsNotNull(bebida);
            CollectionAssert.AreEqual(quantidadesBebidas, bebida.Qtd_Bebida);
        }
    }
}
