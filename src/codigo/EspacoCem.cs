using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabalhoPOOList;

namespace trabalhoPOOList
{
    public class EspacoCem : Espaco // Cria o objeto espa�o para cem pessoas
    {
        private const int Capacidade = 100;

        public EspacoCem()
        {
            this.CapacidadeDoEspaco = EspacoCem.Capacidade;
            this.Preco = 10000 ;
        }

    }
}
