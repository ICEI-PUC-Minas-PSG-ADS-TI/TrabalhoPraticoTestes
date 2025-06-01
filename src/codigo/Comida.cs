using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public abstract class Comida
    {
        public virtual Dictionary<string, double> Itens_Comida { get; protected set; }

        protected abstract Dictionary<string, double> SetDadosSalgados();
    }
}
