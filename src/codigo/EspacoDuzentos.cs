using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabalhoPOOList;

namespace trabalhoPOO
{
    public class EspacoDuzentos : Espaco // Cria o objeto espaço para duzentas pessoas
    {
        private const int Capacidade = 200;
        public EspacoDuzentos() 
        {
            this.CapacidadeDoEspaco = EspacoDuzentos.Capacidade;
            this.Preco = 17000;
        }
    }
}
