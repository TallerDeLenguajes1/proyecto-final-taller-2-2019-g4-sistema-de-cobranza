using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Helpers
{
    public static class ArchivoDeConeccion
    {

        /// <summary>
        /// Lee un archivo y debuelve una lista con las direcciones
        /// </summary>
        /// <returns></returns>
        public static List<string> ExtraerDirecciones ()
        {
            List<string> direcciones = new List<string>();
            // intenta leer un archivo
            try
            {
                string str="";
                var file = new StreamReader (File.Open("coneccion.txt",FileMode.Open));
                while ( file.Peek() >=0 )
                {
                    str = file.ReadLine();
                    direcciones.Add(str);
                }
                return direcciones;
            }
            // si falla la lectura o no se encuentra el archivo 
            //se crea uno con una direccion por defecto
            catch (Exception)
            {
                using (StreamWriter sw = new StreamWriter("conecciones.txt"))
                {
                    sw.Write("server = localhost; userid = root; database = cobranza");
                    sw.Close();
                }
                direcciones.Add("server = localhost; userid = root; database = cobranza");
                return direcciones;
                throw;
            }
        }
    }
}
