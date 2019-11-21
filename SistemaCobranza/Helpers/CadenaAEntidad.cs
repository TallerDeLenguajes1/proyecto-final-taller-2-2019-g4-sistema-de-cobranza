using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Helpers
{
    public static class CadenaAEntidad
    {
        public static Empresa StringToEmpresa(string Item)
        {
            Empresa empresaX = new Empresa();
            string[] separados;
            char[] separator = { '|', ' ' };
            separados = Item.Split(separator);
            empresaX.Cuit = separados[0];
            empresaX.Nombre = separados[1];
            return empresaX;
        }
    }
}
