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
                string ruta = "Registros " + DateTime.Now.ToString("d-M-yyy--H-mm-ss");
                Microsoft.Win32.SaveFileDialog FileName;
                FileName = new Microsoft.Win32.SaveFileDialog();
                FileName.AddExtension = true;
                FileName.DefaultExt = "xlsx";
                FileName.FileName = ruta;
                if (FileName.ShowDialog() == true)
                {   
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
                    excel.Cells[1, 6] = "Resultado";
                    excel.Cells[1, 7] = "Observación";
                    excel.Cells[1, 8] = "Usuario";
                    excel.Cells[1, 9] = "Fecha";

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
                        excel.Cells[fila, 8] = item.Usuario.Nombre;
                        excel.Cells[fila, 9] = item.FechaHora;
                        fila++;
                    }
                    libroexcel.SaveAs(FileName.FileName);
                    libroexcel.Close();
                    excel.Quit();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "Error al exportar registros.");
            }
        }

    }

}
