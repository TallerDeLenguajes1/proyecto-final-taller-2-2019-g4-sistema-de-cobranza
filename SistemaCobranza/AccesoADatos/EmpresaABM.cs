using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos
{
    public static class EmpresaABM
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Devuelve una lista con todas las empresas en la base de datos
        /// </summary>
        /// <returns>Lista de empresas</returns>
        public static List<Empresa> listaEmpresas()
        {

            try
            {
                List<Empresa> empresas = new List<Empresa>();
                Empresa empresaX;
                Conexion con = new Conexion();

                string sql = "select * from empresa";
                var cmd = new MySqlCommand(sql, con.Connection);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    empresaX = new Empresa();
                    empresaX.Cuit = dr.GetString("cuit");
                    empresaX.Nombre = dr.GetString("nombre");
                    empresas.Add(empresaX);
                }
                dr.Close();
                con.Close();
                return empresas;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// Inserta un objeto empresa en base de datos 
        /// </summary>
        /// <param name="empresaX">Objeto empresa</param>
        public static void InsertarEmpresa(Empresa empresaX)
        {

            try
            {
                Conexion con = new Conexion();
                string sql = @"Insert into empresa(cuit,nombre) values(@Cuit, @Nombre)";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Cuit", empresaX.Cuit);
                cmd.Parameters.AddWithValue("@Nombre", empresaX.Nombre);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
        /// <summary>
        /// Modifica el nombre de una empresa a partir de su cuit
        /// </summary>
        /// <param name="empresaX"></param>
        public static void ModificarEmpresa(Empresa empresaX)
        {
            try
            {
                Conexion con = new Conexion();
                string sql = @"UPDATE empresa SET nombre = @Nombre WHERE cuit = @Cuit";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Cuit", empresaX.Cuit);
                cmd.Parameters.AddWithValue("@Nombre", empresaX.Nombre);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
        /// <summary>
        /// Borra una empresa de la BD segun su cuit
        /// </summary>
        /// <param name="empresaX"></param>
        public static void BorrarEmpresa(Empresa empresaX)
        {
            try
            {
                Conexion con = new Conexion();
                string sql = @"DELETE FROM empresa WHERE cuit = @Cuit";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Cuit", empresaX.Cuit);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Busca en la base de datos y retorna un objeto empresa segun su cuit
        /// </summary>
        /// <param name="cuit">Cuit</param>
        /// <returns>Empresa</returns>
        public static Empresa EmpresaPorCuit(string cuit)
        {
            try
            {
                Empresa empresaX;
                Conexion con = new Conexion();
                string sql = "select * from empresa where cuit='" + cuit + "'";
                var cmd = new MySqlCommand(sql, con.Connection);
                var dr = cmd.ExecuteReader();
                dr.Read();
                empresaX = new Empresa();
                empresaX.Cuit = dr.GetString("cuit");
                empresaX.Nombre = dr.GetString("nombre");

                dr.Close();
                con.Close();
                return empresaX;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }
        public static List<Empresa> EmpresasPorAtributo(string atributo,string valor)
        {
            try
            {
                Empresa empresaX;
                List<Empresa> listaEmpresa = new List<Empresa>();
                Conexion con = new Conexion();
                string sql = "select * from empresa where '" + atributo + "' like '" + valor + "'";
                var cmd = new MySqlCommand(sql, con.Connection);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    empresaX = new Empresa();
                    empresaX.Cuit = dr.GetString("cuit");
                    empresaX.Nombre = dr.GetString("nombre");
                    listaEmpresa.Add(empresaX);
                }
                dr.Close();
                con.Close();
                return listaEmpresa;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }
    }
}
