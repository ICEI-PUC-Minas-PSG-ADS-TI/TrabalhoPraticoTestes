using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class ComidaStandard : Comida
    {
        public ComidaStandard()
        {
            this.Itens_Comida = SetDadosSalgados();
        }

        // cria o kit de comida standard 
        // Regra de negócio ( as comidas são específicas para cada tipo de evento)
        protected override Dictionary<string, double> SetDadosSalgados()
        {
            Dictionary<string, double> dadosItens = new Dictionary<string, double>();
            dadosItens.Add("Coxinha, Kibe, Empadinha, Pão de queijo", 40);
           
            return dadosItens;
        }
    }
}
