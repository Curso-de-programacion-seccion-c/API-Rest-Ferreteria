using ApiRestFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Data
{
    public class FormaPagoRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public ApiResponse<List<Models.FormasPagoDB>> GetFormaPago()
        {
            try
            {
                List<Models.FormasPagoDB> formasPagoDBs = new List<Models.FormasPagoDB>();
                using (var conection = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("Sp_FormaPago_ObtenerTodas", conection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                formasPagoDBs.Add(new Models.FormasPagoDB
                                {
                                    idFormaPago = reader.GetByte(0),
                                    NombreFormaPago = reader.GetString(1),
                                    Descripcion = reader.GetString(2)
                                });
                            }
                        }
                    }
                }
                if (formasPagoDBs.Count == 0)
                {
                    return new ApiResponse<List<FormasPagoDB>>
                    {
                        StatusCode = 404,
                        Message = "Formas de pago no encontradas.",
                        data = null
                    };
                }

                return new ApiResponse<List<FormasPagoDB>>
                {
                    StatusCode = 200,
                    Message = "Formas de pago encontradas.",
                    data = formasPagoDBs
                };

            }
            catch (SqlException SqlEX)
            {

                return new ApiResponse<List<FormasPagoDB>>
                {
                    StatusCode = 400,
                    Message = SqlEX.Message,
                    data = null
                };
            }

            }
            public ApiResponse<FormasPagoDB> ObtenerPorId(byte id)
        {
            try
            {
                FormasPagoDB formaPago = null;
                using (var connection = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("Sp_FormaPago_ObtenerPorId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFormaPago", id);
                    cmd.Parameters.AddWithValue("@nombre", string.Empty);
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            formaPago = new FormasPagoDB
                            {
                                idFormaPago = reader.GetByte(0),
                                NombreFormaPago = reader.GetString(1),
                                Descripcion = reader.GetString(2)
                            };
                        }
                    }
                }

                if (formaPago == null)
                {
                    return new ApiResponse<FormasPagoDB>
                    {
                        StatusCode = 404,
                        Message = "Forma de pago no encontrada.",
                        data = null
                    };
                }

                return new ApiResponse<FormasPagoDB>
                {
                    StatusCode = 200,
                    Message = "Forma de pago encontrada.",
                    data = formaPago
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<FormasPagoDB>
                {
                    StatusCode = 400,
                    Message = sqlEx.Message,
                    data = null
                };
            }
        }

        public ApiResponse<string> Crear(FormasPagoDB formaPago)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("Sp_FormaPago_Crear", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreFormaPago", formaPago.NombreFormaPago);
                    cmd.Parameters.AddWithValue("@Descripcion", formaPago.Descripcion ?? (object)DBNull.Value);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return new ApiResponse<string>
                {
                    StatusCode = 200,
                    Message = "Forma de pago creada exitosamente.",
                    data = null
                };
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
        }

        public ApiResponse<string> Actualizar(FormasPagoDB formaPago)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("Sp_FormaPago_Actualizar", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFormaPago", formaPago.idFormaPago);
                    cmd.Parameters.AddWithValue("@NombreFormaPago", formaPago.NombreFormaPago);
                    cmd.Parameters.AddWithValue("@Descripcion", formaPago.Descripcion ?? (object)DBNull.Value);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return new ApiResponse<string>
                {
                    StatusCode = 200,
                    Message = "Forma de pago actualizada exitosamente.",
                    data = null
                };
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
        }

        public ApiResponse<string> Eliminar(byte id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("Sp_FormaPago_Eliminar", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFormaPago", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return new ApiResponse<string>
                {
                    StatusCode = 200,
                    Message = "Forma de pago eliminada exitosamente.",
                    data = null
                };
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
        }
    }
}