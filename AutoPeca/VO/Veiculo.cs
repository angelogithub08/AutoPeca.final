﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.VO
{
    public class Veiculo :BaseVO
    {

        public int codigo { get; set; }
        public string modelo { get; set; }
        public string potencia { get; set; }
        public int ano { get; set; }
        public Fabricante fabricante { get; set; } 
    }
}
