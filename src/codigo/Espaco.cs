using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace trabalhoPOOList
{
    public abstract class Espaco // cria um objeto espaco com atributos comuns para uso das classes derivadas
    {
        public int CapacidadeDoEspaco { get; protected set; }
        public double Preco { get; protected set; }

        public Espaco()
        {
            this.CapacidadeDoEspaco = 0;
            this.Preco = 0;
        }

    }

   
}
