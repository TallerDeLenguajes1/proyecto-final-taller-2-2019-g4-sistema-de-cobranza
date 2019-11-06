using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Helpers
{
    public static class Contrasena
    {
        public static bool VerifyMd5Hash(string x, string y)
        {
            return 0 == StringComparer.OrdinalIgnoreCase.Compare(x, y); //compara las cadenas de texto ingresadas 
        }
        public static string getmd5(string contra)
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
