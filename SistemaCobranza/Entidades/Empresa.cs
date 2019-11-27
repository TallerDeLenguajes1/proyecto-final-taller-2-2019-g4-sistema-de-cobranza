using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Empresa
    {
        public string Cuit { get; set; }
        public string Nombre { get; set; }
        public override string ToString()
        {
            string Pasaje = Cuit + "/" + Nombre;
            return Pasaje;
        }
    }
    
}
