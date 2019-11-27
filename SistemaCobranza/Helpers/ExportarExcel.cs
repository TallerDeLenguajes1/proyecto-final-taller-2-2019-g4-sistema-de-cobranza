using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.IO;

namespace Helpers
{
    public static class ExprotarExcel
    {
        /// <summary>
        /// Recibe una lista de registros para exportar a un archivo excel
        /// </summary>
        /// <param name="registros"></param>
        public static void ExportarRegistro(List<Registro> registros)
        {
            string ruta = "Registros" + DateTime.Now.ToString("d-M-yyy--H-mm-ss") + ".csv";
            //crea y escrive un archivo .csv 
            //en el cual cada archivo se identifica con la fecha y hora que fue creado
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                sw.Write("Empresa;");
                sw.Write("Deudor;");
                sw.Write("Monto;"); 
                sw.Write("Telefono;");
                sw.WriteLine("DNI;");
                foreach (var item in registros)
                {
                    sw.Write($"{item.Deuda.Empresa.Nombre};");
                    sw.Write($"{item.Deuda.Deudor.ApellidoNombre};");
                    sw.Write($"{item.Deuda.Monto};");
                    sw.Write($"{item.Deuda.Deudor.Telefono};");
                    sw.WriteLine($"{item.Deuda.Deudor.Dni};");
                }
            }

        }

    }
}
