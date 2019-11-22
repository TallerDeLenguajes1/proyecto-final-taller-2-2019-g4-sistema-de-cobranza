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
            char[] separator = {'/'};
            string[] separados = Item.Split(separator);
            empresaX.Cuit = separados[0];
            empresaX.Nombre = separados[1];
            return empresaX;
        }
        public static Deudor StringToDeudor(string Item)
        {
            Deudor deudorX = new Deudor();
            char[] separator = { '/' };
            string[] separados = Item.Split(separator);
            deudorX.Dni = separados[0];
            deudorX.ApellidoNombre = separados[1];
            deudorX.Telefono = separados[2];
            return deudorX;
        }
        public static string[] StringToDeuda(string Item)
        {
            char[] separator = { '/' };
            string[] separados = Item.Split(separator);
            return separados;
        }
    }
}
