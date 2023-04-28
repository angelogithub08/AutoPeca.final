using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.VO
{
   public class Pedidos
    {
        public int cod { get; set; }
        public Clientes codigodocliente { get; set; }
        public Pecas codigodapeca { get; set; }
        public string datetime { get; set; }
    }
}
