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
            if(c.Flag == 1)
            {
                try
                {
                    string str = getmd5(u.Contrasena);
                    cmd = new MySqlCommand("Insert into usuario(user,contraseña,nivel) values('" + u.Nombre + "','" + str + "'," + 1 + ")", c.Connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("exito 2");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("No sé", ex.ToString());
                    logger.Error(ex, "Error al Crear Usuario");
                }
            }
        }
        
        private string getmd5(string contra)
        {
            MD5 md5Convert = MD5.Create();//Crea el objeto md5
            //Convierte el string contra en array de bytes y lo codifica
            byte[] data = md5Convert.ComputeHash(Encoding.UTF8.GetBytes(contra));
            //Creo un nuevo string para recibir los bytes y convertirlo a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // Recorre cada byte del codigo cifrado y transforma c/uno a cadena hexadecimal
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            md5Convert.Dispose();
            // Y devuelvo la cadena en cadena hexadecimal
            return sBuilder.ToString();
        }
    }
}
