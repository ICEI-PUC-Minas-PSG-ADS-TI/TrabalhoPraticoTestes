using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class CasamentoStandard : Evento
    {
        // Cria o evento com seus itens, tipo do evento, comidas e bebidas disponíveis para o evento
        // Regra de negócio ( cada tipo de evento terá seus itens específicos)
        public CasamentoStandard(List<int> quantidadesBebidas)
        {
            this.TipoFesta = "Casamento standard";
            this.DadosUtensilios = SetItens();
            this.Comida = new ComidaStandard();
            this.Bebida = new BebidaStandard(quantidadesBebidas);
        }

        protected Dictionary<string,double> SetItens()
        {
            Dictionary<string,double> dadosItens = new Dictionary<string,double>();
            dadosItens.Add("Itens de mesa", 50);
            dadosItens.Add("Decoração", 50);
            dadosItens.Add("Bolo", 10);
            dadosItens.Add("Música", 20);

            return dadosItens;
        }
    }
}
