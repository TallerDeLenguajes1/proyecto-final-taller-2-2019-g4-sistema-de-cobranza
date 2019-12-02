using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.IO;
using Microsoft.Office.Interop.Excel;

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
            //crea y escrive un archivo Excel
            //en el cual cada archivo se identifica con la fecha y hora que fue creado

            //using (StreamWriter sw = new StreamWriter(ruta))
            //{
            //    sw.Write("Empresa;");
            //    sw.Write("Deudor;");
            //    sw.Write("Monto;"); 
            //    sw.Write("Telefono;");
            //    sw.WriteLine("DNI;");
            //    foreach (var item in registros)   
            //    {
            //        sw.Write($"{item.Deuda.Empresa.Nombre};");
            //        sw.Write($"{item.Deuda.Deudor.ApellidoNombre};");
            //        sw.Write($"{item.Deuda.Monto};");
            //        sw.Write($"{item.Deuda.Deudor.Telefono};");
            //        sw.WriteLine($"{item.Deuda.Deudor.Dni};");
            //    }
            //}

            Microsoft.Office.Interop.Excel.Application excel;
            excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);

            excel.Cells.Width(6);
            excel.Cells[1,1] = "Empresa";
            excel.Cells[1, 2] = "Monto";
            excel.Cells[1, 3] = "Deudor";
            excel.Cells[1, 4] = "Telefono";
            excel.Cells[1, 5] = "DNI";

            int fila = 2;
            foreach (var item in registros)
            {
                excel.Cells[fila, 1] = item.Deuda.Empresa.Nombre;
                excel.Cells[fila, 2] = item.Deuda.Deudor.ApellidoNombre;
                excel.Cells[fila, 3] = item.Deuda.Monto;
                excel.Cells[fila, 4] = item.Deuda.Deudor.Telefono;
                excel.Cells[fila, 5] = item.Deuda.Deudor.Dni;
                fila++;
            }

            excel.SaveWorkspace(ruta);



        }

    }
}
