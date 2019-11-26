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
                logger.Error(ex.ToString(), "Error al buscar deudores");
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
                logger.Error(ex.ToString(), "Error al Insertar el deudor: " + deudorX.ApellidoNombre);
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
        /// <summary>
        /// Retorna una lsita con los deudores acorde a los datos introducidos
        /// </summary>
        /// <param name="atributo"> recibe dni o nombre</param>
        /// <returns>Lista Deudores</returns>
        public static List<Deudor> DeudorPorAtributo(string atributo, string valor)
        {
            try
            {
                Deudor deudorX;
                List<Deudor> Deudores = new List<Deudor>();
                Conexion con = new Conexion();
                string sql;
                if (atributo == "dni") sql = @"SELECT * FROM deudor WHERE dni like @Valor"; 
                else sql = @"SELECT * FROM deudor WHERE ApellidoNombre like @Valor";
                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Valor", "%" + valor + "%");
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
                logger.Error(ex.ToString(),"Error al buscar deudores con los atributos "+ atributo+", " + valor +"");
                return null;
            }
        }
        /// <summary>
        /// Modifica datos de una deudor a partir de su dni
        /// </summary>
        /// <param name="deudorX"></param>
        public static void ModificarDeudor(Deudor deudorX)
        {
            try
            {
                Conexion con = new Conexion();
                string sql = @"UPDATE deudor SET apellidonombre = @ApellidoNombre, telefono = @Telefono WHERE dni = @Dni";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@ApellidoNombre", deudorX.ApellidoNombre);
                cmd.Parameters.AddWithValue("@Telefono", deudorX.Telefono);
                cmd.Parameters.AddWithValue("@Dni", deudorX.Dni);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(),"Error al modificar el deudor: "+ deudorX.ApellidoNombre);
            }
        }
        /// <summary>
        /// Borra una deudor de la BD segun su dni
        /// </summary>
        /// <param name="deudorX"></param>
        public static void BorrarDeudor(Deudor deudorX)
        {
            try
            {
                Conexion con = new Conexion();
                string sql = @"DELETE FROM deudor WHERE dni = @Dni";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Dni", deudorX.Dni);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "Error al modificar el deudor: " + deudorX.ApellidoNombre);
            }
        }

    }
}
