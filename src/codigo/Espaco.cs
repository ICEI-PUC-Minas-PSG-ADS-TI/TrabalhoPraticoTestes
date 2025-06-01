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

        // Construtor antigo, sem parâmetros. É bom mantê-lo para flexibilidade.
        public Espaco()
        {
            this.CapacidadeDoEspaco = 0;
            this.Preco = 0;
        }

        // === ADICIONE ESTE NOVO CONSTRUTOR ===
        // Ele aceita os parâmetros necessários para criar um Espaco já com valores.
        public Espaco(double preco, int capacidade)
        {
            this.Preco = preco;
            this.CapacidadeDoEspaco = capacidade;
        }
    }
}