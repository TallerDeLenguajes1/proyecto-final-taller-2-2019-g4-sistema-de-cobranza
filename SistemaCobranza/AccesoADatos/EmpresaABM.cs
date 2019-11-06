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
        public static List<Empresa> listaEmpresas()
        {
            // TODO  verificar conexion y clase empresa 


            try
            {
                List<Empresa> empresas = new List<Empresa>();
                Empresa empresaX;
                Conexion con = new Conexion();
                con.Conectar();

                string sql = "select * from Empresa";
                var cmd = new MySqlCommand(sql, con.cn);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    empresaX = new Empresa();
                    empresaX.Cuit = dr.GetString("cuit");
                    empresaX.Nombre = dr.GetString("nombre");
                    empresas.Add(empresaX);
                }
                dr.Close();
                con.Desconectar();
                return empresas;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static void InsertarEmpresa(Empresa empresaX)
        {

            try
            {
                Conexion con = new Conexion();
                string sql = @"Inset into alumno(cuit,nombre) values(@Cuit, @Nombre)";
                con.Conectar();

                var cmd = new MySqlCommand(sql, con.cn);
                cmd.Parameters.AddWithValue("@cuit", empresaX.Cuit);
                cmd.Parameters.AddWithValue("@Nombre", empresaX.Nombre);
                cmd.ExecuteNonQuery();

                con.Desconectar();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static Empresa EmpresaPorCuit(string cuit)
        {
            try
            {
                Empresa empresaX;
                Conexion con = new Conexion();
                con.Conectar();

                string sql = "select * from Empresa where cuit='" + cuit + "'";
                var cmd = new MySqlCommand(sql, con.cn);
                var dr = cmd.ExecuteReader();

                dr.Read();

                empresaX = new Empresa();
                empresaX.Cuit = dr.GetString("cuit");
                empresaX.Nombre = dr.GetString("nombre");

                dr.Close();
                con.Desconectar();
                return empresaX;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }


        }

    }
}
