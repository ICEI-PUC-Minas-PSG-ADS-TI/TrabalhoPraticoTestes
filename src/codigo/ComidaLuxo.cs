using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class ComidaLuxo : Comida
    {
        // cria o kit de comida luxo 
        // Regra de negócio ( as comidas são específicas para cada tipo de evento)
        public ComidaLuxo()
        {
            this.Itens_Comida = SetDadosSalgados();
        }
        protected override Dictionary<string, double> SetDadosSalgados()
        {
            Dictionary<string, double> dadosItens = new Dictionary<string, double>();
            dadosItens.Add("Croquete carne seca, Barquetes legumes, Empadinha gourmet, Cestinha bacalhau", 48);

            return dadosItens;
        }
    }
}
