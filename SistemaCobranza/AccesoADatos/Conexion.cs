using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NLog;

namespace AccesoADatos
{
    public class Conexion
    {
        MySqlConnection connection;
        public Conexion()
        {
            string str;
            str = "server=localhost;userid=root;database=cobranza";

            try
            {
                connection = new MySqlConnection(str);
                connection.Open();
                MessageBox.Show("exito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto con la base de datos.");
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex, "No se pudo conectar con la base de datos.");
            }
        }
        public void Close()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto con la base de datos.");
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex, "No se pudo desconectar con la base de datos (tal vez no estaba conectada en primer lugar?).");
            }
            
        }

    }
}
