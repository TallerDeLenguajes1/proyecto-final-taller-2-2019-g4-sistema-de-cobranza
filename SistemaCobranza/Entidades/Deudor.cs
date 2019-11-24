using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Deudor
    {
        public string Dni { get; set; }
        public string ApellidoNombre { get; set; }
        public string Telefono { get; set; }

        public override string ToString()
        {
            return Dni + " | " + ApellidoNombre + " | " + Telefono;
        }
    }
}
