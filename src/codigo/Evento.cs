using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class Evento
    {
        public string TipoFesta { get; protected set; }
        public Dictionary<string, double> DadosUtensilios { get; protected set; }
        public Comida Comida { get; protected set; }
        public Bebida Bebida { get; protected set; }


    }
}
