using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class EventoLivre : Evento
    {
        // Cria o evento com seus itens, tipo do evento, comidas e bebidas disponíveis para o evento
        // Regra de negócio ( cada tipo de evento terá seus itens específicos)
        // Evento livre será somente locação do espaço
        public EventoLivre() 
        {
            this.TipoFesta = "Evento livre";
        }
    }
}
