﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    public abstract class Bebida
    {
        public virtual Dictionary<string, double> Lista_Bebidas { get; protected set; }
        public virtual List<int> Qtd_Bebida { get; protected set; }

        protected abstract Dictionary<string, double> SetBebidas();
    }
}
