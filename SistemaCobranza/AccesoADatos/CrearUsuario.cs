using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Entidades;
using System.Security.Cryptography;
using System.Windows.Forms;
using NLog;

namespace AccesoADatos
{
    public class CrearUsuario
    {
        Conexion c = new Conexion();
        MySqlCommand cmd;
        Logger logger = LogManager.GetCurrentClassLogger();
        public void Crearuser(Usuario u)
        {
            if (c.Flag == 1)
            {
                try
                {
                    string str = getmd5(u.Contrasena);//usa la funcion getmd5 para codificar la contraseña y para almacenarla en la base de datos
                    cmd = new MySqlCommand("Insert into usuario(user,contrasena,nivel) values('" + u.Nombre + "','" + str + "'," + 1 + ")", c.Connection);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo crear el usuario", ex.ToString());
                    logger.Error(ex, "Error al Crear Usuario");
                }
            }
            else
            {
                MessageBox.Show("No hay conexión con la base de datos.");
                logger.Error("No hay conexión con la base de datos.");
            }

        }
        
        private string getmd5(string contra)
        {
            MD5 md5Convert = MD5.Create();//Crea el objeto md5
            //Convierte el string contra en array de bytes y lo codifica
            byte[] data = md5Convert.ComputeHash(Encoding.UTF8.GetBytes(contra));
            //Creo un nuevo string para recibir los bytes y convertirlo a string.
            StringBuilder sBuilder = new StringBuilder();
            // Recorre cada byte del codigo cifrado y transforma c/uno a cadena hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            md5Convert.Dispose();//libera los recuros usados por esta instancia
            // Y devuelvo la cadena en cadena hexadecimal
            return sBuilder.ToString();
        }
    }
}
