using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabalhoPOO;
using trabalhoPOOList;

namespace Trabalho_POO
{
    public abstract class Reserva
    {
        public RepositorioMongoDB repo = new RepositorioMongoDB();
        public static DateTime DataDaUltimaReserva;
        public virtual DateTime DataReservada { get; set; }
        public virtual Espaco Espaco { get;  set; }

        public virtual int QuantidadeDeReservas { get; set; }

        // O a primeira data disponível
        // Regra de negócio ( 30 dia após o dia da marcação sexta/sábado).
        protected DateTime EncontrarDataDisponivel()
        {
            DateTime dataTemp = new DateTime();
            dataTemp = DateTime.Today.AddDays(30);

            while (dataTemp.DayOfWeek != DayOfWeek.Friday && dataTemp.DayOfWeek != DayOfWeek.Saturday)
            {
                dataTemp = dataTemp.AddDays(1);
            }

            return dataTemp;
        }
        public abstract void Reservar();

        // Encontrar uma data disponível após a primeira data passada
        // Regra de negócio ( se for sexta pula somente um dia, se for sábado pula 6 pa o próximo final de semana)
        protected DateTime ObterProximaData(DateTime dataTemp)
        {

            if (dataTemp.DayOfWeek == DayOfWeek.Friday)
            {
                dataTemp = dataTemp.AddDays(1);
            }
            else
            {
                dataTemp = dataTemp.AddDays(6);
            }
            return dataTemp;
        }
        public abstract void SetDataReserva();

    }
}
