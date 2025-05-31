using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class BebidaStandard : Bebida
    {
        // cria as bebidas standard 
        // Regra de negócio ( as bebidas são específicas para cada tipo de evento)
        public BebidaStandard(List<int> quantidade) 
        {
            this.Lista_Bebidas = SetBebidas();
            this.Qtd_Bebida = quantidade;
        }

        protected override Dictionary<string, double> SetBebidas()
        {
            Dictionary<string, double> bebidas = new Dictionary<string, double>();

            bebidas.Add("Água sem gás (1,5L)", 5);
            bebidas.Add("Suco (1L)", 7);
            bebidas.Add("Refrigerante (2L)", 8);
            bebidas.Add("Cerveja Comum (600ml)", 20);
            bebidas.Add("Espumante nacional (750ml)", 80);

            return bebidas;
        }
    }
}
