using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos
{
    public static class DeudorABM
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static List<Deudor> ListaDeudores()
        {
            // TODO  verificar conexion y clase Deudor 

            try
            {
                List<Deudor> Deudors = new List<Deudor>();
                Deudor deudorX;
                Conexion con = new Conexion();
                con.Conectar();

                string sql = "select * from Deudor";
                var cmd = new MySqlCommand(sql, con.cn);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    deudorX = new Deudor();
                    deudorX.Cuit = dr.GetString("cuit");
                    deudorX.Nombre = dr.GetString("nombre");
                    Deudors.Add(deudorX);
                }
                dr.Close();
                con.Desconectar();
                return Deudors;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static void InsertarDeudor(Deudor deudorX)
        {

            try
            {
                Conexion con = new Conexion();
                string sql = @"Inset into alumno(dni,apellidoNombre,telefono) values(@Dni, @ApellidoNombre, @Telefono)";
                con.Conectar();

                var cmd = new MySqlCommand(sql, con.cn);
                cmd.Parameters.AddWithValue("@cuit", deudorX.Dni);
                cmd.Parameters.AddWithValue("@Nombre", deudorX.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", deudorX.Telefono);
                cmd.ExecuteNonQuery();

                con.Desconectar();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static Deudor DeudorPorDni(string dni)
        {
            try
            {
                Deudor deudorX;
                Conexion con = new Conexion();
                con.Conectar();

                string sql = "select * from Deudor where cuit='" + dni + "'";
                var cmd = new MySqlCommand(sql, con.cn);
                var dr = cmd.ExecuteReader();

                dr.Read();

                deudorX = new Deudor();
                deudorX.Dni = dr.GetString("dni");
                deudorX.ApellidoNombre = dr.GetString("ApellidoNombre");

                dr.Close();
                con.Desconectar();
                return deudorX;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }


        }
    }
}
