using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class ComidaPremier : Comida
    {
        // cria o kit de comida Premier 
        // Regra de negócio ( as comidas são específicas para cada tipo de evento)
        public ComidaPremier()
        {
            this.Itens_Comida = SetDadosSalgados();
        }
        protected override Dictionary<string, double> SetDadosSalgados()
        {
            Dictionary<string, double> dadosItens = new Dictionary<string, double>();
            dadosItens.Add("Canapé, Tartine, Bruschetta, Espetinho caprese", 60);

            return dadosItens;
        }
    }
}
