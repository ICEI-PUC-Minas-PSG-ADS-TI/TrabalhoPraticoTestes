using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class BebidaLuxo: Bebida
    {
        // cria as bebidas luxo 
        // Regra de negócio ( as bebidas são específicas para cada tipo de evento)
        public BebidaLuxo(List<int> quantidade)
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
            bebidas.Add("Cerveja Artesanal (600ml)", 30);
            bebidas.Add("Espumante nacional (750ml)", 80);
            bebidas.Add("Espumante importado (750ml)", 140);

            return bebidas;
        }

       
    }
}
