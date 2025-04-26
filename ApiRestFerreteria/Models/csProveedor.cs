using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ApiRestFerreteria.Models;
using System.Collections.Generic;

namespace ApiRestFerreteria.Proveedor
{
    public class csProveedor
    {
        private string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Obtener todos los proveedores
        public ApiResponse<List<Models.Proveedor>> obtenerProveedores()
        {
            try
            {
                List<Models.Proveedor> proveedors = new List<Models.Proveedor>();
                using (var conn = new SqlConnection(conexion))
                using (var cmd = new SqlCommand("ObtenerProveedores", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                proveedors.Add(new Models.Proveedor
                                {
                                    IdProveedor = reader.GetByte(0),
                                    NombreProveedor = reader.GetString(1),
                                    Telefono = reader.GetString(2),
                                    NombreContacto = reader.GetString(3)
                                });
                            }
                        }
                    }
                }

                if (proveedors.Count <= 0)
                {
                    return new ApiResponse<List<Models.Proveedor>>
                    {
                        StatusCode = 404,
                        Message = "No se encontraron proveedores",
                        data = null
                    };
                }

                return new ApiResponse<List<Models.Proveedor>>
                {
                    StatusCode = 200,
                    Message = "Proveedores encontrados",
                    data = proveedors
                };
            }
            catch (Exception SqlEx)
            {
                return new ApiResponse<List<Models.Proveedor>>
                {
                    StatusCode = 400,
                    Message = SqlEx.Message,
                    data = null
                };
            }
        }

        // Obtener proveedor por id
        public ApiResponse<Models.Proveedor> obtenerProveedor(int idProveedor)
        {
            try
            {
                Models.Proveedor proveedor = new Models.Proveedor();
                using (var conn = new SqlConnection(conexion))
                using (var cmd = new SqlCommand("SpObtenerProveedor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                proveedor.IdProveedor = reader.GetByte(0);
                                proveedor.NombreProveedor = reader.GetString(1);
                                proveedor.Telefono = reader.GetString(2);
                                proveedor.NombreContacto = reader.GetString(3);
                            }
                        }
                    }
                }

                if (proveedor.IdProveedor == 0)
                {
                    return new ApiResponse<Models.Proveedor>
                    {
                        StatusCode = 404,
                        Message = "No se encontró el proveedor",
                        data = null
                    };
                }

                return new ApiResponse<Models.Proveedor>
                {
                    StatusCode = 200,
                    Message = "Proveedor encontrado",
                    data = proveedor
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Models.Proveedor>
                {
                    StatusCode = 400,
                    Message = ex.Message,
                    data = null
                };
            }
        }

        // Crear un nuevo proveedor
        public ApiResponse<string> CrearProveedor(Models.Proveedor proveedor)
        {
            try
            {
                using(var conn = new SqlConnection(conexion))
                using(var cmd = new SqlCommand("InsertarProveedor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreProveedor", proveedor.NombreProveedor);
                    cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                    cmd.Parameters.AddWithValue("@NombreContacto", proveedor.NombreContacto);

                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return new ApiResponse<string>
                        {
                            StatusCode = 200,
                            Message = "Proveedor creado exitosamente",
                            data = null
                        };
                    }
                    else
                    {
                        return new ApiResponse<string>
                        {
                            StatusCode = 400,
                            Message = "Error al crear el proveedor",
                            data = null
                        };
                    }
                }
            }
            catch (SqlException sqlEX)
            {
                return new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = sqlEX.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    StatusCode = 500,
                    Message = "Error al crear el proveedor: " + ex.Message,
                    data = null
                };
            }
        }

        // Actualizar un proveedor
        public ApiResponse<string> ActualizarProveedor(Models.Proveedor proveedor)
        {
            try
            {
                using(var conn = new SqlConnection(conexion))
                using(var cmd = new SqlCommand("ActualizarProveedor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProveedor", proveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("@NombreProveedor", proveedor.NombreProveedor);
                    cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                    cmd.Parameters.AddWithValue("@NombreContacto", proveedor.NombreContacto);

                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return new ApiResponse<string>
                        {
                            StatusCode = 200,
                            Message = "Proveedor actualizado exitosamente",
                            data = null
                        };
                    }
                    else
                    {
                        return new ApiResponse<string>
                        {
                            StatusCode = 400,
                            Message = "Error al actualizar el proveedor",
                            data = null
                        };
                    }

                }
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = sqlEx.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    StatusCode = 500,
                    Message = "Error al actualizar el proveedor: " + ex.Message,
                    data = null
                };
            }
        }

        // Eliminar un proveedor
        public ApiResponse<bool> EliminarProveedor(int idProveedor)
        {
            try
            {
                using (var conn = new SqlConnection(conexion))
                using (var cmd = new SqlCommand("EliminarProveedor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    conn.Open();

                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return new ApiResponse<bool>
                        {
                            StatusCode = 200,
                            Message = "Proveedor eliminado exitosamente",
                            data = true
                        };
                    }
                    else
                    {
                        return new ApiResponse<bool>
                        {
                            StatusCode = 400,
                            Message = "Error al eliminar el proveedor",
                            data = false
                        };
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 400,
                    Message = sqlEx.Message,
                    data = false
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 500,
                    Message = "Error al eliminar el proveedor: " + ex.Message,
                    data = false
                };
            }
        }
    }
}