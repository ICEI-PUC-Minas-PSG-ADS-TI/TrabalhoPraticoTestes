using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhoPOOList
{
    public class Espaco
    {
        public virtual int CapacidadeDoEspaco { get;  set; }
        public virtual double Preco { get; set; }

        // Construtor antigo, sem par�metros. � bom mant�-lo para flexibilidade.
        public Espaco()
        {
            this.CapacidadeDoEspaco = 0;
            this.Preco = 0;
        }

        // === ADICIONE ESTE NOVO CONSTRUTOR ===
        // Ele aceita os par�metros necess�rios para criar um Espaco j� com valores.
        public Espaco(double preco, int capacidade)
        {
            this.Preco = preco;
            this.CapacidadeDoEspaco = capacidade;
        }
    }
}