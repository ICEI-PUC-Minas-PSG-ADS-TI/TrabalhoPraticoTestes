using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class CasamentoLuxo : Evento
    {
        // Cria o evento com seus itens, tipo do evento, comidas e bebidas disponíveis para o evento
        // Regra de negócio ( cada tipo de evento terá seus itens específicos)
        public CasamentoLuxo(List<int> quantidadesBebidas)
        {
            this.TipoFesta = "Casamento luxo";
            this.DadosUtensilios = SetItens();
            this.Comida = new ComidaLuxo();
            this.Bebida = new BebidaLuxo(quantidadesBebidas);
        }
        protected Dictionary<string, double> SetItens()
        {
            Dictionary<string, double> dadosItens = new Dictionary<string, double>();
            dadosItens.Add("Itens de mesa", 75);
            dadosItens.Add("Decoração", 75);
            dadosItens.Add("Bolo", 15);
            dadosItens.Add("Música", 25);

            return dadosItens;
        }
    }
}
