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
                string sql = @"Insert into registro(fechahora,observacion,resultado,dni,cuit,id_usuario) values(@FechaHora,@Observacion,@Resultado,@Dni,@Cuit,@Usuario)";
                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@FechaHora", registroX.FechaHora);
                cmd.Parameters.AddWithValue("@Observacion", registroX.Observacion);
                cmd.Parameters.AddWithValue("@Resultado", registroX.Resultado);
                cmd.Parameters.AddWithValue("@Dni", registroX.Deuda.Deudor.Dni);
                cmd.Parameters.AddWithValue("@Cuit", registroX.Deuda.Empresa.Cuit);
                cmd.Parameters.AddWithValue("@Usuario", registroX.Usuario.Id_usuario);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException ex)
            {
                logger.Error(ex.ToString(),"Error al crear registro.");
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
                List<Usuario> Prueba;
                if (atributo == "usuario")
                {
                    Prueba = UsuarioABM.UsuarioPorNombre(valor);
                    Conexion con = new Conexion();
                    for(int i= 0; i<Prueba.Count; i++)
                    {
                        string sql = @"SELECT * FROM registro WHERE id_usuario LIKE @Valor";
                        var cmd = new MySqlCommand(sql, con.Connection);
                        cmd.Parameters.AddWithValue("@Valor","%" + Prueba[i].Id_usuario + "%");
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            registroX = new Registro();
                            registroX.Id_Registro = dr.GetInt32("id_registro");
                            registroX.FechaHora = dr.GetString("fechaHora");
                            registroX.Observacion = dr.GetString("observacion");
                            registroX.Resultado = dr.GetString("resultado");
                            registroX.Deuda = DeudaABM.DeudaPorDniCuit(dr.GetString("dni"), dr.GetString("cuit"));
                            registroX.Usuario = UsuarioABM.UsuarioPorId(dr.GetString("id_usuario"));
                            Registros.Add(registroX);
                        }
                        dr.Dispose();
                        con.Close();
                    }
                    return Registros;
                }
                else
                {
                    string sql;
                    Conexion con = new Conexion();
                    if(atributo == "dni")  sql= "SELECT * FROM registro WHERE dni LIKE @Valor";
                    else sql = "SELECT * FROM registro WHERE cuit LIKE @Valor";
                    var cmd = new MySqlCommand(sql, con.Connection);
                    cmd.Parameters.AddWithValue("@Valor","%" + valor + "%");
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        registroX = new Registro();
                        registroX.Id_Registro = dr.GetInt32("id_registro");
                        registroX.FechaHora = dr.GetString("fechaHora");
                        registroX.Observacion = dr.GetString("observacion");
                        registroX.Resultado = dr.GetString("resultado");
                        registroX.Deuda = DeudaABM.DeudaPorDniCuit(dr.GetString("dni"), dr.GetString("cuit"));
                        registroX.Usuario = UsuarioABM.UsuarioPorId(dr.GetString("id_usuario"));
                        Registros.Add(registroX);
                    }
                    dr.Dispose();
                    con.Close();
                    return Registros;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "Error al Buscar registro de " + atributo + ": "+ valor +".");
                return null;
            }
        }
        public static List<Registro> ListaRegistros()
        {
            try
            {
                List<Registro> Registros = new List<Registro>();
                Registro registroX;
                Conexion con = new Conexion();


                string sql = "select * from registro";
                var cmd = new MySqlCommand(sql, con.Connection);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    registroX = new Registro();
                    registroX.Id_Registro = dr.GetInt32("id_registro");
                    registroX.FechaHora = dr.GetString("fechahora");
                    registroX.Observacion = dr.GetString("observacion");
                    registroX.Resultado = dr.GetString("resultado");
                    registroX.Deuda = DeudaABM.DeudaPorDniCuit(dr.GetString("dni"), dr.GetString("cuit"));
                    registroX.Usuario = UsuarioABM.UsuarioPorId(dr.GetString("id_usuario"));
                    Registros.Add(registroX);
                }
                dr.Close();
                con.Close();

                return Registros;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "Error al cargar registro.");
                return null;
            }
        }

        /// <summary>
        /// Modifica un registro a partir de su id
        /// </summary>
        /// <param name="registroX"></param>
        public static void ModificarRegistro(Registro registroX)
        {
            try
            {
                Conexion con = new Conexion();
                string sql = @"UPDATE registro SET observacion = @Observacion, fechahora = @FechaHora, resultado = @Resultado, cuit = @Cuit, dni = @Dni, id_usuario = @Usuario  WHERE id_registro = @IdRegistro";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@Observacion", registroX.Observacion);
                cmd.Parameters.AddWithValue("@FechaHora", registroX.FechaHora);
                cmd.Parameters.AddWithValue("@Resultado", registroX.Resultado);
                cmd.Parameters.AddWithValue("@Dni", registroX.Deuda.Deudor.Dni);
                cmd.Parameters.AddWithValue("@Cuit", registroX.Deuda.Empresa.Cuit);
                cmd.Parameters.AddWithValue("@Usuario", registroX.Usuario.Id_usuario);
                cmd.Parameters.AddWithValue("@Usuario", registroX.Id_Registro);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
        /// <summary>
        /// Borra una Registro de la BD segun su id
        /// </summary>
        /// <param name="registroX"></param>
        public static void BorrarRegistro(Registro registroX)
        {
            try
            {
                Conexion con = new Conexion();
                string sql = @"DELETE FROM registro WHERE id_registro = @IdRegistro";

                var cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@IdRegistro", registroX.Id_Registro);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

    }
}
