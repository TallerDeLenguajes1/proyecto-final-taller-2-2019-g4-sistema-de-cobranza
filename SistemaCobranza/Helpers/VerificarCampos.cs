using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class VerificarCampos
    {
        public static string VerificarEmpresa(string cuit, string nombre)
        {
            string estado = "true";
            if (!Verificarnum(cuit)) estado = "Cuit debe ser de sólo numeros.";
            if (!Verificarcaracteres(nombre)) estado = "El nombre debe ser de sólo letras.";
            return estado;
        }
        public static string VerificarDeudor(string dni,string nombreyape,string telefono)
        {
            string estado = "true";
            if (!Verificarnum(dni)) estado = "El DNI debe ser de sólo numeros.";
            if (!Verificarnum(telefono)) estado = "Cuit debe ser de sólo numeros.";
            if (!Verificarcaracteres(nombreyape)) estado = "Debe haber al menos un nombre y un apellido y solo letras.";
            return estado;
        }
        /*public static string VerificarRegistro()
        {
            string estado = "true";
            if (!Verificarnum()) estado = "Cuit debe ser de sólo numeros.";
            if (!Verificarcaracteres()) estado = "El nombre debe ser sólo letras.";
            return estado;
        }*/
        public static bool Verificarnum(string prueba)
        {
            bool estado = true;
            int result;
            if (!int.TryParse(prueba,out result)) estado = false;
            return estado;
        }
        public static bool Verificarcaracteres(string prueba)
        {
            bool estado = true;
            if (prueba.All(char.IsLetter)) estado = false;
            return estado;
        }

    }
}
