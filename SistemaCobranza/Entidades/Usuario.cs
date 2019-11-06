using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Entidades
{
    public class Usuario
    {
        private string nombre { get; set; }
        private string contrasena { get; set; }

        public string Contrasena { get => contrasena; set => contrasena = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
