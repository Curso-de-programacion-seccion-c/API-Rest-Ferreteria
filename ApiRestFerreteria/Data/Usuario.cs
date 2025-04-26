using ApiRestFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Data
{
    public class Usuario
    {
        // Crear nuevo usuario
        public Models.Usuario.responseUsuario CrearUsuario(Models.Usuario model)
        {
            Models.Usuario.responseUsuario result = new Models.Usuario.responseUsuario();
            string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    // Validar si el IdEmpleado ya está asociado a otro usuario
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM Usuario WHERE IdEmpleado = @IdEmpleado", con);
                    checkCmd.Parameters.AddWithValue("@IdEmpleado", model.IdEmpleado);
                    con.Open();
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    con.Close();

                    if (count > 0)
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = "El IdEmpleado ya está asociado a otro usuario.";
                        return result;
                    }

                    // Crear el nuevo usuario
                    SqlCommand cmd = new SqlCommand("sp_CrearUsuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEmpleado", model.IdEmpleado);
                    cmd.Parameters.AddWithValue("@CodigoUsuario", model.CodigoUsuario);
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@UserPassword", model.UserPassword);
                    cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                    con.Open();
                    var resultId = cmd.ExecuteNonQuery();
                    if (resultId != 0)
                    {
                        result.respuesta = Convert.ToInt32(resultId);
                        result.descripcion_respuesta = "Usuario creado exitosamente";
                    }
                    else
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = "Error al insertar el usuario";
                    }
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Ocurrió un error: " + ex.Message;
                }
            }
            return result;
        }
        // Obtener Usuarios
        public List<Models.Usuario> ObtenerUsuarios()
        {
            List<Models.Usuario> usuarios = new List<Models.Usuario>();
            string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarios", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Usuario usuario = new Models.Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                                NombreEmpleado = reader["NombreEmpleado"].ToString(),
                                CodigoUsuario = reader["CodigoUsuario"].ToString(),
                                Username = reader["Username"].ToString(),
                                IsActive = Convert.ToByte(reader["IsActive"])
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    throw new Exception("Error al obtener los usuarios: " + ex.Message);
                }
            }
            return usuarios;
        }

        // Obtener Usuario por Id
        public Models.Usuario ObtenerUsuarioPorId(int IdUsuario)
        {
            Models.Usuario usuario = null;
            string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarioPorId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Models.Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                                NombreEmpleado = reader["NombreEmpleado"].ToString(),
                                CodigoUsuario = reader["CodigoUsuario"].ToString(),
                                Username = reader["Username"].ToString(),
                                IsActive = Convert.ToByte(reader["IsActive"])
                            };
                        }
                        else
                        {
                            throw new Exception("Usuario no encontrado.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    throw new Exception("Error al obtener el usuario por Id: " + ex.Message);
                }
            }
            return usuario;
        }

        // Actualizar Usuario
        public Models.Usuario.responseUsuario ActualizarUsuario(Models.Usuario model)
        {
            Models.Usuario.responseUsuario result = new Models.Usuario.responseUsuario();
            string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    // Validar si el IdEmpleado ya está asociado a otro usuario diferente
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM Usuario WHERE IdEmpleado = @IdEmpleado AND IdUsuario <> @IdUsuario", con);
                    checkCmd.Parameters.AddWithValue("@IdEmpleado", model.IdEmpleado);
                    checkCmd.Parameters.AddWithValue("@IdUsuario", model.IdUsuario);
                    con.Open();
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    con.Close();

                    if (count > 0)
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = "El IdEmpleado ya está asociado a otro usuario.";
                        return result;
                    }

                    // Actualizar el usuario
                    SqlCommand cmd = new SqlCommand("sp_ActualizarUsuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", model.IdUsuario);
                    cmd.Parameters.AddWithValue("@IdEmpleado", model.IdEmpleado);
                    cmd.Parameters.AddWithValue("@CodigoUsuario", model.CodigoUsuario);
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@UserPassword", model.UserPassword);
                    cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Si no se lanza excepción, asumimos éxito
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Usuario actualizado exitosamente";
                }
                catch (SqlException sqlEx)
                {
                    // Verificar si el error proviene de RAISERROR
                    if (sqlEx.Number == 50000) // Código de error para RAISERROR
                    {
                        result.respuesta = 1;
                        result.descripcion_respuesta = sqlEx.Message;
                    }
                    else
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = "Error en la base de datos: " + sqlEx.Message;
                    }
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Ocurrió un error: " + ex.Message;
                }
            }
            return result;
        }
        // Desactivar Usuario
        public Models.Usuario.responseUsuario DesactivarUsuario(int IdUsuario)
        {
            Models.Usuario.responseUsuario result = new Models.Usuario.responseUsuario();
            string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DesactivarUsuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    con.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result.respuesta = 1;
                        result.descripcion_respuesta = "Usuario desactivado exitosamente";
                    }
                    else
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = "No se encontró el usuario para desactivar";
                    }
                }
                catch (SqlException sqlEx)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Error en la base de datos: " + sqlEx.Message;
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Ocurrió un error: " + ex.Message;
                }
            }
            return result;
        }
        //Eliminar Usuario
        public Models.Usuario.responseUsuario EliminarUsuario(int IdUsuario)
        {
            Models.Usuario.responseUsuario result = new Models.Usuario.responseUsuario();
            string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarUsuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Si no se lanza excepción, asumimos éxito
                    result.respuesta = 1;
                    result.descripcion_respuesta = "Usuario eliminado exitosamente";
                }
                catch (SqlException sqlEx)
                {
                    // Verificar si el error proviene de RAISERROR
                    if (sqlEx.Number == 50000) // Código de error para RAISERROR
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = sqlEx.Message;
                    }
                    else
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = "Error en la base de datos: " + sqlEx.Message;
                    }
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Ocurrió un error: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            return result;
        }
        // Login Usuario
        public Models.Usuario.responseUsuario LoginUsuario(string username, string userPassword, string codigoUsuario)
        {
            Models.Usuario.responseUsuario result = new Models.Usuario.responseUsuario();
            string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_LoginUsuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@UserPassword", userPassword);
                    cmd.Parameters.AddWithValue("@CodigoUsuario", codigoUsuario);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.respuesta = 1;
                            result.descripcion_respuesta = "Login exitoso";
                            result.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                            result.Username = reader["Username"].ToString();
                            result.CodigoUsuario = reader["CodigoUsuario"].ToString();
                            result.IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]);
                            result.NombreEmpleado = reader["Nombre"].ToString();
                            result.ApellidoEmpleado = reader["Apellido"].ToString();
                            result.Puesto = reader["Puesto"].ToString();
                            result.CorreoElectronico = reader["CorreoElectronico"].ToString();
                            result.Telefono = reader["Telefono"].ToString();
                            result.IdRol = Convert.ToInt32(reader["Idrol"]);
                            result.NombreRol = reader["NombreRol"].ToString();
                            result.Sueldo = Convert.ToDecimal(reader["Sueldo"]);
                        }
                        else
                        {
                            result.respuesta = 0;
                            result.descripcion_respuesta = "Credenciales incorrectas";
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Error en la base de datos: " + sqlEx.Message;
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Ocurrió un error: " + ex.Message;
                }
            }
            return result;
        }
        
     
       

        












    }
}