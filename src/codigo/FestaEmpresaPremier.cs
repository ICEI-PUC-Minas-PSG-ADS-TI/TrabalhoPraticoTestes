using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class FestaEmpresaPremier : Evento
    {
        // Cria o evento com seus itens, tipo do evento, comidas e bebidas disponíveis para o evento
        // Regra de negócio ( cada tipo de evento terá seus itens específicos)
        public FestaEmpresaPremier(List<int> quantidadesBebidas)
        {
            this.TipoFesta = "Festa de empresa premier";
            this.DadosUtensilios = SetItens();
            this.Comida = new ComidaPremier();
            this.Bebida = new BebidaPremier(quantidadesBebidas);
        }
        protected Dictionary<string, double> SetItens()
        {
            Dictionary<string, double> dadosItens = new Dictionary<string, double>();
            dadosItens.Add("Música", 30);

            return dadosItens;
        }
    }
}
