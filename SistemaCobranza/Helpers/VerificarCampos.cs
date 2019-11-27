using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Text.RegularExpressions;

namespace Helpers
{
    public static class VerificarCampos
    {
        public static string VerificarEmpresa(Empresa empresaX)
        {
            string estado = "true";
            if (!Verificarnum(empresaX.Cuit)) estado = "Cuit debe ser de sólo numeros.";
            if (!Verificarcaracteres(empresaX.Nombre)) estado = "El nombre debe ser de sólo letras.";
            return estado;
        }
        public static string VerificarDeudor(string dni,string nombreyape,string telefono)
        {
            string estado = "true";
            if (!Verificarnum(dni)) estado = "El DNI debe ser de sólo numeros.";
            if (!Verificarnum(telefono)) estado = "Telefono debe ser de sólo numeros.";
            if (!Verificarcaracteres(nombreyape)) estado = "Debe haber al menos un nombre y un apellido y solo letras.";
            return estado;
        }
        public static bool Verificarnum(string prueba)
        {
            bool estado = true;
            int result;
            if (!int.TryParse(prueba,out result)) estado = false;
            return estado;
        }
        public static bool Verificarcaracteres(string prueba)
        {
            string pattern = @"^[A-Za-z ]+$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(prueba) == false)
            {
               return false;
            }
            return true;
        }

    }
}
