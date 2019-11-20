using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos
{
    public static class RegistroABM
    {
        // TODO  verificar conexion y clase Registro 
        
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Inserta un registro en la Base de Datos
        /// </summary>
        /// <param name="registroX">Recibe un objeto de la Clase Registro</param>
        public static void InsertarRegistro(Registro registroX)
        {

            try
            {
                Conexion con = new Conexion();
                string sql = @"Inset into alumno(fechaHora,observacion,resultado,dni,cuit) values(@FechaHora,@Observacion,@Resultado,@Dni,@Cuit)";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@FechaHora", registroX.FechaHora);
                cmd.Parameters.AddWithValue("@Observacion", registroX.Observacion);
                cmd.Parameters.AddWithValue("@Resultado", registroX.Resultado);
                cmd.Parameters.AddWithValue("@Dni", registroX.Deuda.Deudor.Dni);
                cmd.Parameters.AddWithValue("@Cuit", registroX.Deuda.Empresa.Cuit);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// Busca registros por el atributo indicado y su correspondiente valor. Ej: RegsitroPorAtributo(Dni,25655846) 
        /// </summary>
        /// <param name="atributo">Atributo puede tomar: dni, cuit, usuario</param>
        /// <param name="valor">valor de dicho atributo</param>
        /// <returns>Lista de registros</returns>
        public static List<Registro> RegistrosPorAtributo(string atributo, string valor)
        {
            try
            {
                List<Registro> Registros = new List<Registro>();
                Registro registroX;
                Conexion con = new Conexion();


                string sql = "select * from Registro where @Atributo like @Valor ";
                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Atributo", atributo);
                cmd.Parameters.AddWithValue("@Valor", valor);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    registroX = new Registro();
                    registroX.Id_Registro = dr.GetInt32("cuit");
                    registroX.FechaHora = dr.GetDateTime("fechaHora");
                    registroX.Observacion = dr.GetString("observacion");
                    registroX.Resultado = dr.GetString("resultado");
                    registroX.Deuda.Deudor = DeudorABM.DeudorPorDni( dr.GetString("dni"));
                    registroX.Deuda.Empresa = EmpresaABM.EmpresaPorCuit(dr.GetString("cuit"));
                    registroX.Usuario = UsuarioABM.UsuarioPorId(dr.GetString("id_usuario"));
                    Registros.Add(registroX);
                }
                dr.Close();
                con.Close();
                
                return Registros;
            }
            catch (Exception ex)
            {
                logger.Error(ex,"Error al crear registro.");
                return null;
            }
        }
    
    }
}
