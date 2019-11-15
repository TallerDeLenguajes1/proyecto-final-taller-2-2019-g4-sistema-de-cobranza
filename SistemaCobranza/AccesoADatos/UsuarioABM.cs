using Entidades;
using Helpers;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesoADatos
{
    public static class UsuarioABM
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public static void Loguear(Usuario u)
        {

            try
            {
                Conexion c = new Conexion();
                MySqlCommand cmd = new MySqlCommand("Select * from usuario where user='" + u.Nombre + "'", c.Connection);
                MySqlDataReader dr = cmd.ExecuteReader();

                dr.Read();
                string passEnBasedeD = dr["contrasena"].ToString();
                string contracodif = Contrasena.Getmd5(u.Contrasena);//codificar contraseña del usuario
                if (Contrasena.VerifyMd5Hash(passEnBasedeD, contracodif)) u.nivel = Convert.ToInt32(dr["nivel"]); //si las contraseñas son iguales devuelve el nivel
                else MessageBox.Show("Contraseña Incorrecta");


                dr.Dispose();//libera los recursos usados por ésta instancia
                c.Close();
                logger.Trace("Usuario logueado : " + u.Nombre);

                u.Contrasena = "";
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario incorrecto.", ex.ToString());
                logger.Error(ex.ToString(), "Usuario incorrecto, Contraseña incorrecta o no se pudo conectar con el servidor.");
            }

        }

        public static void Crearuser(Usuario u)
        {
            Conexion c = new Conexion();
            MySqlCommand cmd;

            try
            {
                string str = Contrasena.Getmd5(u.Contrasena);//usa la funcion getmd5 para codificar la contraseña y para almacenarla en la base de datos
                cmd = new MySqlCommand("Insert into usuario(user,contrasena,nivel) values('" + u.Nombre + "','" + str + "'," + 1 + ")", c.Connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo crear el usuario", ex.ToString());
                logger.Error(ex, "Error al Crear Usuario");
            }
        }
    }
}

