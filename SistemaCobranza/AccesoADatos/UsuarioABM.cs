using Entidades;
using Helpers;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (Contrasena.VerifyMd5Hash(passEnBasedeD, contracodif)) u.Nivel = Convert.ToInt32(dr["nivel"]); //si las contraseñas son iguales devuelve el nivel
                u.Id_usuario = Convert.ToInt32(dr["id_usuario"]);
                dr.Dispose();//libera los recursos usados por ésta instancia
                c.Close();
                logger.Trace("Usuario logueado : " + u.Nombre);

                u.Contrasena = "";
                cmd.Dispose();
            }
            catch (Exception ex)
            {
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
                cmd = new MySqlCommand("Insert into usuario(user,contrasena,nivel) values('" + u.Nombre + "','" + str + "'," + u.Nivel + ")", c.Connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "Error al Crear Usuario");
            }
        }
        /// <summary>
        /// Busca y retorna un usuario por su user/nombre
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>Usuario</returns>
        public static List<Usuario> UsuarioPorNombre(string nombre)
        {
            Usuario UsuarioX;
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                Conexion con = new Conexion();

                string sql = @"select * from Usuario where user like @Usuario";
                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Usuario", "%"+nombre+"%");
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    UsuarioX = new Usuario();
                    UsuarioX.Id_usuario = dr.GetInt32("id_usuario");
                    UsuarioX.Nombre = dr.GetString("user");
                    UsuarioX.Nivel = dr.GetInt16("nivel");
                    usuarios.Add(UsuarioX);
                }
                dr.Dispose();
                con.Close();
                return usuarios;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "Error al buscar usuarios.");
                return null;
            }
        }
        public static Usuario UsuarioPorId(string id)
        {
            Usuario UsuarioX;
            try
            {
                Conexion con = new Conexion();

                string sql = "select * from usuario where id_usuario = "+ id +"";
                var cmd = new MySqlCommand(sql, con.Connection);
                var dr = cmd.ExecuteReader();
                dr.Read();
                UsuarioX = new Usuario();
                UsuarioX.Id_usuario = dr.GetInt32("id_usuario");
                UsuarioX.Nombre = dr.GetString("user");
                UsuarioX.Nivel = dr.GetInt32("nivel");
                dr.Dispose();
                con.Close();
                return UsuarioX;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "Error al buscar el usuario.");
                return null;
            }
        }

        /// <summary>
        /// Modifica datos de un usuario a partir de su id
        /// </summary>
        /// <param name="usuarioX"></param>
        public static void ModificarUsuario(Usuario usuarioX)
        {
            try
            {
                Conexion con = new Conexion();
                string sql = @"UPDATE usuario SET user = @Nombre, contrasena = @Contrasena, nivel = @Nivel WHERE id_usuario = @IdUsuario";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Nombre", usuarioX.Nombre);
                cmd.Parameters.AddWithValue("@Contrasena", usuarioX.Contrasena);
                cmd.Parameters.AddWithValue("@Nivel", usuarioX.Nivel);
                cmd.Parameters.AddWithValue("@IdUsuario", usuarioX.Id_usuario);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }


        /// <summary>
        /// Borra un usuario de la BD segun su id
        /// </summary>
        /// <param name="usuarioX"></param>
        public static void BorrarUsuario(Usuario usuarioX)
        {
            try
            {
                Conexion con = new Conexion();
                string sql = @"DELETE FROM usuario WHERE id_usuario = @IdUsuario";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@IdUsuario", usuarioX.Id_usuario);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

    }
}

