using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.IO;
using Microsoft.Office.Interop.Excel;
using NLog;

namespace Helpers
{
    public static class ExprotarExcel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Recibe una lista de registros para exportar a un archivo excel
        /// </summary>
        /// <param name="registros"></param>
        public static void ExportarRegistro(List<Registro> registros)
        {
            try
            {
                string ruta = "Registros" + DateTime.Now.ToString("d-M-yyy--H-mm-ss") + ".xls";

                Application excel;
                Workbook libroexcel;
                Worksheet hojaexcel;
                object misValue = System.Reflection.Missing.Value;
                excel = new Application();
                libroexcel = excel.Workbooks.Add(misValue);
                hojaexcel = (Worksheet)libroexcel.Worksheets.get_Item(1);

                excel.Cells[1, 1] = "Empresa";
                excel.Cells[1, 2] = "Monto";
                excel.Cells[1, 3] = "Deudor";
                excel.Cells[1, 4] = "Telefono";
                excel.Cells[1, 5] = "DNI";
                excel.Cells[1, 3] = "Resultado";
                excel.Cells[1, 4] = "Observación";
                excel.Cells[1, 5] = "Usuario";

                int fila = 2;
                foreach (var item in registros)
                {
                    excel.Cells[fila, 1] = item.Deuda.Empresa.Nombre;
                    excel.Cells[fila, 2] = item.Deuda.Deudor.ApellidoNombre;
                    excel.Cells[fila, 3] = item.Deuda.Monto;
                    excel.Cells[fila, 4] = item.Deuda.Deudor.Telefono;
                    excel.Cells[fila, 5] = item.Deuda.Deudor.Dni;
                    excel.Cells[fila, 6] = item.Resultado;
                    excel.Cells[fila, 7] = item.Observacion;
                    excel.Cells[fila, 8] = item.Usuario;
                    fila++;
                }
                libroexcel.Save();
                libroexcel.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "Error al exportar registros.");
            }
        }

    }

}
