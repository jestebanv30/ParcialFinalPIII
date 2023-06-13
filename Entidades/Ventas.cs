using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Ventas
    {
        public Sedes Sedes { get; set; }
        public Productos Productos { get; set; }
        public double Valor { get; set; }
    }
}
