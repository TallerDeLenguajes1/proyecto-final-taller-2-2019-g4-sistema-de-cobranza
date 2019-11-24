using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos
{
    public class DeudaABM
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Inserta una Deuda nueva en la base de datos 
        /// </summary>
        /// <param name="deudaX">Objeto tipo Deuda</param>
        public static void InsertarDeuda(Deuda deudaX)
        {

            try
            {
                Conexion con = new Conexion();
                string sql = @"Insert into deuda(dni,cuit,monto) values(@Dni,@Cuit,@Monto)";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Dni", deudaX.Deudor.Dni);
                cmd.Parameters.AddWithValue("@Cuit", deudaX.Empresa.Cuit);
                cmd.Parameters.AddWithValue("@Monto", deudaX.Monto);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// Busca deudas por dni o cuit y retorna una lista con las coincidencias
        /// </summary>
        /// <param name="atributo">Dni/Cuit</param>
        /// <param name="valor">Valor</param>
        /// <returns>Lista Deudas</returns>
        public static List<Deuda> deudasPorAtributo(string atributo, string valor)
        {
            try
            {
                List<Deuda> deudas = new List<Deuda>();
                Deuda deudaX;
                Conexion con = new Conexion();
                string sql = "select * from deuda where @Atributo = @Valor ";
                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Atributo", atributo);
                cmd.Parameters.AddWithValue("@Valor", valor);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    deudaX = new Deuda();
                    deudaX.Empresa = EmpresaABM.EmpresaPorCuit(dr.GetString("cuit"));
                    deudaX.Deudor = DeudorABM.DeudorPorDni( dr.GetString("dni") );
                    deudaX.Monto = dr.GetDouble("monto");
                    deudas.Add(deudaX);
                }
                dr.Close();
                con.Close();
                return deudas;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }
        public static Deuda DeudaPorDniCuit(string dni, string cuit)
        {
            try
            {
                Deuda deudaX;
                Conexion con = new Conexion();
                string sql = "select * from deuda where dni = @Dni and cuit = @Cuit";
                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Dni", dni);
                cmd.Parameters.AddWithValue("@Cuit", cuit);
                var dr = cmd.ExecuteReader();

                dr.Read();
                deudaX = new Deuda();
                deudaX.Empresa = EmpresaABM.EmpresaPorCuit(dr.GetString("cuit"));
                deudaX.Deudor = DeudorABM.DeudorPorDni(dr.GetString("dni"));
                deudaX.Monto = dr.GetDouble("monto");
            
                
                dr.Close();
                con.Close();
                return deudaX;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(),"No se cargo deuda");
                return null;
            }
        }
        /// <summary>
        /// Consigue una lista de deudas de la base de datos y la retorna.
        /// </summary>
        /// <returns>Lista Deudas</returns>
        public static List<Deuda> ListadeDeudas()
        {
            try
            {
                List<Deuda> deudas = new List<Deuda>();
                Deuda deudaX;
                Conexion con = new Conexion();
                string sql = "select * from deuda";
                var cmd = new MySqlCommand(sql, con.Connection);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    deudaX = new Deuda();
                    deudaX.Empresa = EmpresaABM.EmpresaPorCuit(dr.GetString("cuit"));
                    deudaX.Deudor = DeudorABM.DeudorPorDni(dr.GetString("dni"));
                    deudaX.Monto = dr.GetDouble("monto");
                    deudas.Add(deudaX);
                }
                dr.Dispose();
                con.Close();
                return deudas;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica el monto de una deuda a partir de su cuit
        /// </summary>
        /// <param name="deudaX"></param>
        public static void ModificarDeuda(Deuda deudaX)
        {
            try
            {
                Conexion con = new Conexion();
                string sql = @"UPDATE deuda SET monto = @Monto WHERE cuit = @Cuit and dni = @Dni";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Monto", deudaX.Monto);
                cmd.Parameters.AddWithValue("@Cuit", deudaX.Empresa.Cuit);
                cmd.Parameters.AddWithValue("@Dni", deudaX.Deudor.Dni);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
        /// <summary>
        /// Borra una Deuda de la BD segun su cuit y dni
        /// </summary>
        /// <param name="deudaX"></param>
        public static void BorrarDeuda(Deuda deudaX)
        {
            try
            {
                Conexion con = new Conexion();
                string sql = @"DELETE FROM deuda WHERE cuit = @Cuit and dni = @Dni";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Cuit", deudaX.Empresa.Cuit);
                cmd.Parameters.AddWithValue("@Dni", deudaX.Deudor.Dni);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}
