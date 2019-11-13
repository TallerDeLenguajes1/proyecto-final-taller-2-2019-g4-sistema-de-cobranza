using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Deuda
    {
        public Empresa Empresa{ get; set; }
        public Deudor Deudor { get; set; }
        public double Monto { get; set; }
    }
}
