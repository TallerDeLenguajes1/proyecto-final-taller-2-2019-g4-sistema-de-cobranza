﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Entidades
{
    public class Usuario
    {
        public int Id_usuario{ get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public int Nivel{ get; set; }
    }
}
