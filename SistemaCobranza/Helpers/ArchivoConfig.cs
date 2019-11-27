using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Helpers
{
    public static class ArchivoConfig
    {
        public static string[] configfile()
        {
            string line = "";
            string[] dude = null;
            try
            {
                string path = "C:\\Config";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (File.Exists("C:\\Config\\configuracion.config"))
                {
                    StreamReader fileexist = new StreamReader("C:\\Config\\configuracion.config");
                    int cont = 0;
                    while((line = fileexist.ReadLine()) != null)
                    {
                        if (cont == 1)
                        {
                            dude = dude.Concat(line.Split(' ')).ToArray();
                        }
                        else
                        {
                            dude = line.Split(' ');
                            cont++;
                        }
                    }
                    fileexist.Close();
                    string[] datos = { dude[2], dude[5], dude[8], dude[11] };
                    return datos;
                }
                else
                {
                    using (StreamWriter sw = File.AppendText("C:\\Config\\configuracion.config"))
                    {
                        sw.WriteLine("Servidor = localhost");
                        sw.WriteLine("Usuario = root");
                        sw.WriteLine("Contraseña = ");
                        sw.WriteLine("Nombre_de_DB = cobranza");
                        sw.Close();
                    }
                    string[] datoscreados = { "localhost", "root", "", "cobranza" };
                    return datoscreados;
                }
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    } 
}
