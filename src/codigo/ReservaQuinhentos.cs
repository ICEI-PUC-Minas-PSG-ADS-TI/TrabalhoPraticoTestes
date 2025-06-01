using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO;
using trabalhoPOO;


namespace trabalhoPOOList
{

    public class ReservaQuinhentos : Reserva
    {
        public ReservaQuinhentos()
        {
            this.Espaco = new EspacoQuinhentos();
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
        // regra de negócio ( espaço para quinhentas pessoas 1 agendamentos por data)
        public override void SetDataReserva()
        {
            DateTime dataTemp;


            if (this.QuantidadeDeReservas > 0)
            {
                dataTemp = ObterProximaData(Reserva.DataDaUltimaReserva);
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
