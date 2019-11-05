using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Entidades;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace AccesoADatos
{
    class LogIn
    {
        MySqlCommand cmd;
        MySqlDataReader dr;
        public int login(MySqlConnection c, Usuario u)
        {
            try
            {
                cmd = new MySqlCommand("Select from Usuarios where Usuario=" + u.Nombre, c);
                dr = cmd.ExecuteReader();
                string passEnBasedeD = dr["contraseña"].ToString();
                if (passEnBasedeD != getmd5(u.Contrasena)) MessageBox.Show("Contraseña Incorrecta"); //
                return Convert.ToInt32(dr["nivel"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario incorrecto.", ex.ToString());
                return 0;
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

            // Y devuelvo la cadena en cadena hexadecimal
            return sBuilder.ToString();
        }
    }
}
