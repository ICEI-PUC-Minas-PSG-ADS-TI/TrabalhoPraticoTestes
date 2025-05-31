using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabalhoPOOList;

namespace trabalhoPOO
{
    public class EspacoCinquenta : Espaco  // Cria o objeto espaço para cinquenta pessoas
    {
        private const int Capacidade = 50;
        public EspacoCinquenta()
        {
            this.CapacidadeDoEspaco = EspacoCinquenta.Capacidade;
            this.Preco = 8000;
        }
    }
}
