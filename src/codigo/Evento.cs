using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class Evento
    {
        public virtual string TipoFesta { get; protected set; }
        public virtual Dictionary<string, double> DadosUtensilios { get; protected set; }
        public virtual Comida Comida { get; protected set; }
        public virtual Bebida Bebida { get; protected set; }

        public Evento() { }
    }
}
