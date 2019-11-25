using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NLog;

namespace AccesoADatos
{
    public class Conexion
    {
        MySqlConnection connection;
        public MySqlConnection Connection
        {
            get { return this.connection; }
        }
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Conexion()
        {
            string str;
            //str = "server=localhost;userid=root;pwd=1234;database=cobranza"; // traer de archivo, con contraseña
            str = "server=localhost;userid=root;database=cobranza"; //sin contraseña
            try
            {
                this.connection = new MySqlConnection(str);
                this.connection.Open();
                
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "No se pudo conectar con la base de datos.");
  
            }
        }
        public void Close()
        {
            try
            {
                this.connection.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "No se pudo desconectar con la base de datos (tal vez no estaba conectada en primer lugar?).");
            }

        }

    }
}