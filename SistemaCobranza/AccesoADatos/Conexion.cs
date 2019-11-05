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
        int flag = 0;
        public int Flag
        {
            get { return flag; }
        }
        public MySqlConnection Connection
        {
            get { return this.connection; }
        }
        Logger logger = LogManager.GetCurrentClassLogger();
        public Conexion()
        {
            string str;
            str = "server=localhost;userid=root;database=cobranza";

            try
            {
                this.connection = new MySqlConnection(str);
                this.connection.Open();
                MessageBox.Show("exito");
                flag = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto con la base de datos.");
                logger.Error(ex, "No se pudo conectar con la base de datos.");
                flag = 0;
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
                logger.Error(ex, "No se pudo desconectar con la base de datos (tal vez no estaba conectada en primer lugar?).");
            }
            
        }

    }
}
