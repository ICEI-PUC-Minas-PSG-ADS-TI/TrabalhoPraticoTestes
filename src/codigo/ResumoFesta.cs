using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public class ResumoFesta
    {
        public string TipoFesta;
        public Dictionary<string, double> Utensilios = new Dictionary<string, double>();
        public double ValorTotalUtensilios;

        public Dictionary<string, double> Comidas = new Dictionary<string, double>();
        public double ValorTotalComidas;

        public List<Bebida> Bebidas = new List<Bebida>();
        public double ValorTotalBebidas;

        public DateTime DataReservada;
        public int CapacidadeEspaco;
        public double ValorEspaco;

        public double ValorTotalFesta;

    }
}
