using Entidades;
using MySql.Data.MySqlClient;
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

        /// <summary>
        /// Retorna una lsita con todos los deudores
        /// </summary>
        /// <returns>Lista Deudores</returns>
        public static List<Deudor> ListaDeudores()
        {
            try
            {
                List<Deudor> Deudores = new List<Deudor>();
                Deudor deudorX;
                Conexion con = new Conexion();

                string sql = "select * from deudor";
                var cmd = new MySqlCommand(sql, con.Connection);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    deudorX = new Deudor();
                    deudorX.Dni = dr.GetString("dni");
                    deudorX.ApellidoNombre = dr.GetString("ApellidoNombre");
                    deudorX.Telefono = dr.GetString("telefono");
                    Deudores.Add(deudorX);
                }
                dr.Close();
                dr.Dispose();

                con.Close();

                return Deudores;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public static void InsertarDeudor(Deudor deudorX)
        {

            try
            {
                Conexion con = new Conexion();
                string sql = @"Insert into deudor(dni,ApellidoNombre,telefono) values(@Dni, @ApellidoNombre, @Telefono)";
                
                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Dni", deudorX.Dni);
                cmd.Parameters.AddWithValue("@ApellidoNombre", deudorX.ApellidoNombre);
                cmd.Parameters.AddWithValue("@Telefono", deudorX.Telefono);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static Deudor DeudorPorDni(string dni)
        {
            Deudor deudorX;
            try
            {
                Conexion con = new Conexion();                

                string sql = "select * from deudor where dni='" + dni + "'"; // agregar parametro
                var cmd = new MySqlCommand(sql, con.Connection);
                var dr = cmd.ExecuteReader();

                dr.Read();

                deudorX = new Deudor();
                deudorX.Dni = dr.GetString("dni");
                deudorX.ApellidoNombre = dr.GetString("ApellidoNombre");
                deudorX.Telefono = dr.GetString("telefono");

                dr.Close();
                con.Close();
                return deudorX;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "Error al buscar deudor");
                return null;
            }
        }
        public static List<Deudor> DeudorPorDniParecidos(string dni)
        {

            try
            {
                Deudor deudorX;
                List<Deudor> Deudores = new List<Deudor>();
                Conexion con = new Conexion();
                string sql = "select * from deudor where dni like '%" + dni + "%'"; // agregar parametro
                var cmd = new MySqlCommand(sql, con.Connection);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    deudorX = new Deudor();
                    deudorX.Dni = dr["dni"].ToString();
                    deudorX.ApellidoNombre = dr["ApellidoNombre"].ToString();
                    deudorX.Telefono = dr["telefono"].ToString();
                    Deudores.Add(deudorX);
                }
                dr.Close();
                con.Close();
                return Deudores;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }
        
    }
}
