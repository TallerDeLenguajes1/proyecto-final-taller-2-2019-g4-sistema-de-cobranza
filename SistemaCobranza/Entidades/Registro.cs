using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Registro
    {
        public int Id_Registro { get; set; }
        public Deuda Deuda { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaHora { get; set; }
        public string Resultado{ get; set; }
        public Usuario Usuario{ get; set; } 
    }
}
