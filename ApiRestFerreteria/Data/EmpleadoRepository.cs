using ApiRestFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Data
{
	public class EmpleadoRepository
	{
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ApiResponse<List<Models.EmpleadoDB>>GetEmpleado()
        {
            try
            {
                List<EmpleadoDB> empleados = new List<EmpleadoDB>();

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_BuscarEmpleados", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                EmpleadoDB empleadoDB = new EmpleadoDB();
                                empleadoDB.idEmpleado = reader.GetInt16(0);
                                empleadoDB.Dpi = reader.GetString(1);
                                empleadoDB.Nombre = reader.GetString(2);
                                empleadoDB.Apellido = reader.GetString(3);
                                empleadoDB.Puesto = reader.GetString(4);
                                empleadoDB.CorreoElectronico = reader.GetString(5);
                                empleadoDB.Telefono = reader.GetString(6);
                                empleadoDB.FechaContratacion = reader.GetDateTime(7).ToString("yyyy-MM-dd");
                                empleadoDB.IdRol = reader.GetByte(8);
                                empleadoDB.nombreRol = reader.GetString(9);
                                empleadoDB.Sueldo = reader.GetDecimal(10);

                                empleados.Add(empleadoDB);
                            }
                        }
                    }
                }

                return new ApiResponse<List<EmpleadoDB>>
                {
                    StatusCode = 200,
                    Message = "Empleados obtenidos exitosamente",
                    data = empleados
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<List<EmpleadoDB>>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<EmpleadoDB>>
                {
                    StatusCode = 500,
                    Message = "Error inesperado: " + ex.Message,
                    data = null
                };
            }
        }

        public ApiResponse<EmpleadoDB> GetEmpleado(byte id)
        {
            try
            {
                EmpleadoDB empleado = null;

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_ObtenerEmpleado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEmpleado", id);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            empleado = new EmpleadoDB
                            {
                                idEmpleado = reader.GetInt16(0),
                                Dpi = reader.GetString(1),
                                Nombre = reader.GetString(2),
                                Apellido = reader.GetString(3),
                                Puesto = reader.GetString(4),
                                CorreoElectronico = reader.GetString(5),
                                Telefono = reader.GetString(6),
                                FechaContratacion = reader.GetDateTime(7).ToString("yyyy-MM-dd"),
                                IdRol = reader.GetByte(8),
                                nombreRol = reader.GetString(9),
                                Sueldo = reader.GetDecimal(10)
                            };
                        }
                    }
                }

                if (empleado == null)
                {
                    return new ApiResponse<EmpleadoDB>
                    {
                        StatusCode = 404,
                        Message = "Empleado no encontrado",
                        data = null
                    };
                }

                return new ApiResponse<EmpleadoDB>
                {
                    StatusCode = 200,
                    Message = "Empleado encontrado",
                    data = empleado
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<EmpleadoDB>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = null
                };
            }
        }

        public ApiResponse<string> CrearEmpleado(CrearEmpleado empleado)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_CrearEmpleado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Dpi", empleado.Dpi);
                    cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                    cmd.Parameters.AddWithValue("@Puesto", empleado.Puesto);
                    cmd.Parameters.AddWithValue("@CorreoElectronico", empleado.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                    cmd.Parameters.AddWithValue("@IdRol", empleado.IdRol);
                    cmd.Parameters.AddWithValue("@FechaContratacion", empleado.FechaContratacion);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return new ApiResponse<string>
                        {
                            StatusCode = 400,
                            Message = "No se pudo crear el empleado",
                            data = null
                        };
                    }

                    return new ApiResponse<string>
                    {
                        StatusCode = 200,
                        Message = "Empleado creado exitosamente",
                        data = null
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<string>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = null
                };
            }
        }

        public ApiResponse<string> ActualizarEmpleado(EmpleadoDB empleado)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_ActualizarEmpleado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEmpleado", empleado.idEmpleado);
                    cmd.Parameters.AddWithValue("@Dpi", empleado.Dpi);
                    cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                    cmd.Parameters.AddWithValue("@Puesto", empleado.Puesto);
                    cmd.Parameters.AddWithValue("@CorreoElectronico", empleado.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                    cmd.Parameters.AddWithValue("@IdRol", empleado.IdRol);
                    cmd.Parameters.AddWithValue("@FechaContratacion", empleado.FechaContratacion);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return new ApiResponse<string>
                        {
                            StatusCode = 400,
                            Message = "No se encontró el empleado para actualizar",
                            data = null
                        };
                    }

                    return new ApiResponse<string>
                    {
                        StatusCode = 200,
                        Message = "Empleado actualizado exitosamente",
                        data = null
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<string>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = null
                };
            }
        }

        public ApiResponse<string> EliminarEmpleado(byte id)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_EliminarEmpleado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEmpleado", id);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return new ApiResponse<string>
                        {
                            StatusCode = 404,
                            Message = "No se encontró el empleado para eliminar",
                            data = null
                        };
                    }

                    return new ApiResponse<string>
                    {
                        StatusCode = 200,
                        Message = "Empleado eliminado exitosamente",
                        data = null
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547)
                {
                    return new ApiResponse<string>
                    {
                        StatusCode = 400,
                        Message = "No se puede eliminar el empleado porque tiene registros relacionados",
                        data = null
                    };
                }

                return new ApiResponse<string>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = null
                };
            }
        }

        public ApiResponse<List<EmpleadoDB>> BuscarEmpleados(string filtro)
        {
            try
            {
                List<EmpleadoDB> empleados = new List<EmpleadoDB>();

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_BuscarEmpleados", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Filtro", filtro ?? string.Empty);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            empleados.Add(new EmpleadoDB
                            {
                                idEmpleado = reader.GetByte(0),
                                Dpi = reader.GetString(1),
                                Nombre = reader.GetString(2),
                                Apellido = reader.GetString(3),
                                Puesto = reader.GetString(4),
                                CorreoElectronico = reader.GetString(5),
                                Telefono = reader.GetString(6)
                                //IdRol = reader.GetByte(7),
                                //FechaContratacion = reader.GetDateTime(8)
                            });
                        }
                    }
                }

                return new ApiResponse<List<EmpleadoDB>>
                {
                    StatusCode = 200,
                    Message = empleados.Count > 0 ? "Búsqueda exitosa" : "No se encontraron resultados",
                    data = empleados
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<List<EmpleadoDB>>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = null
                };
            }
        }
    }

}
