using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabalhoPOOList;

namespace trabalhoPOO
{
    public class EspacoQuinhentos : Espaco // Cria o objeto espaço para quinhentas pessoas
    {
        private const int Capacidade = 500;
        public EspacoQuinhentos()
        {
            this.CapacidadeDoEspaco = EspacoQuinhentos.Capacidade;
            this.Preco = 35000;
        }
    }
}
