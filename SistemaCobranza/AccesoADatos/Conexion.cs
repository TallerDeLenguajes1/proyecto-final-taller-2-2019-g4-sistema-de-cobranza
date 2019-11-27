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
        public static string direccion;

        public MySqlConnection Connection
        {
            get { return this.connection; }
        }
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Conexion()
        {
            if ( direccion == null)
            {
                direccion = Helpers.ArchivoConfig.configfile();
            }
            try
            {
                this.connection = new MySqlConnection(direccion);
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