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
                string sql = @"Inset into deuda(dni,cuit,monto) values(@Dni,@Cuit,@Monto)";

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
        /// <summary>
        /// Busca deudas por dni o cuit y retorna una lista con las coincidencias
        /// </summary>
        /// <param name="atributo">Dni/Cuit</param>
        /// <param name="valor">Valor</param>
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



    }
}
