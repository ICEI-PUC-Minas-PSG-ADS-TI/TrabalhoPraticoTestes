using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO;
using trabalhoPOOList;


namespace trabalhoPOO
{

    public class ReservaDuzentos : Reserva
    {

        public ReservaDuzentos()
        {
            this.Espaco = new EspacoDuzentos();
            var dadosReserva = this.repo.RecuperarDadosReserva(this.Espaco.CapacidadeDoEspaco);

            if (dadosReserva != null)
            {
                Reserva.DataDaUltimaReserva = dadosReserva.DataReservada;
                this.QuantidadeDeReservas = dadosReserva.QtdReservas;
            }
            else
            {
                Reserva.DataDaUltimaReserva = DateTime.Now;
                this.QuantidadeDeReservas = 0;
            }
        }

        public override void Reservar()
        {
            SetDataReserva();
        }

        // Escolhe realiza o agendamento na data apropriada
        // regra de negócio ( espaço para duzentas pessoas 2 agendamentos por data)
        public override void SetDataReserva()
        {
            DateTime dataTemp;

            if (this.QuantidadeDeReservas % 2 == 0 && this.QuantidadeDeReservas > 0)
            {
                dataTemp = ObterProximaData(Reserva.DataDaUltimaReserva);
            }
            else if (this.QuantidadeDeReservas > 0)
            {
                dataTemp = Reserva.DataDaUltimaReserva;
            }
            else
            {
                dataTemp = EncontrarDataDisponivel();
            }

            this.DataReservada = dataTemp;
            this.QuantidadeDeReservas++;
        }


    }
}