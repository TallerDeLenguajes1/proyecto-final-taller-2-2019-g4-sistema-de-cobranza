using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;

namespace Helpers
{
    public static class ArchivoConfig
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static string configfile()
        {
            string line = "";
            string[] dude = null;
            try
            {
                if (File.Exists("configuracion.config"))
                {
                    StreamReader fileexist = new StreamReader("configuracion.config");
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
                    string str;
                    if (datos[2] == "") str = "server=" + datos[0] + ";userid=" + datos[1] + ";database=" + datos[3];
                    else str = "server=" + datos[0] + ";userid=" + datos[1] + ";pwd=" + datos[2] + ";database=" + datos[3];
                return str;
                }
                else
                {
                    using (StreamWriter sw = File.AppendText("configuracion.config"))
                    {
                        sw.WriteLine("Servidor = localhost");
                        sw.WriteLine("Usuario = root");
                        sw.WriteLine("Contraseña = ");
                        sw.WriteLine("Nombre_de_DB = cobranza");
                        sw.Close();
                    }
                    string str = "server=localhost;userid=root;database=cobranza";
                    return str;
                }
                
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "No se pudo leer o crear archivo de configuracion");
                return null;
            }
        }
    } 
}
