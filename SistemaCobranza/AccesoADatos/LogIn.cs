using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Entidades;
using System.Windows.Forms;
using System.Security.Cryptography;
using NLog;

namespace AccesoADatos
{
    public class LogIn
    {
        MySqlCommand cmd;
        MySqlDataReader dr;
        Logger logger = LogManager.GetCurrentClassLogger();
        Conexion c = new Conexion();

        public int Loguear(Usuario u)
        {
            int nivel = 0;
            if (c.Flag == 1)
            {
                try
                {
                    cmd = new MySqlCommand("Select * from usuario where user='" + u.Nombre + "'", c.Connection);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        string passEnBasedeD = dr["contrasena"].ToString();
                        string contracodif = getmd5(u.Contrasena);//codificar contraseña del usuario
                        if (VerifyMd5Hash(passEnBasedeD, contracodif)) nivel = Convert.ToInt32(dr["nivel"]); //si las contraseñas son iguales devuelve el nivel
                        else MessageBox.Show("Contraseña Incorrecta");
                        dr.Dispose();//libera los recursos usados por ésta instancia
                        return nivel;//devuelve el nivel del usuario
                    }
                    c.Close();
                    logger.Error("Usuario no existente : " + u.Nombre);
                    return nivel;
                }
                catch (Exception ex)
                {
                    c.Close();
                    MessageBox.Show("Usuario incorrecto.", ex.ToString());
                    logger.Error(ex.ToString(), "Usuario incorrecto, Contraseña incorrecta o no se pudo conectar con el servidor.");

                    return nivel;
                }
            }
            else
            {
                c.Close();
                return nivel;
            }
        }
        private static bool VerifyMd5Hash(string x, string y)
        {
            return 0 == StringComparer.OrdinalIgnoreCase.Compare(x, y); //compara las cadenas de texto ingresadas 
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
            string builded = sBuilder.ToString();
            md5Convert.Dispose();//libera los recuros usados por esta instancia
            // Y devuelvo la cadena en cadena hexadecimal
            return builded;
        }
    }
}
