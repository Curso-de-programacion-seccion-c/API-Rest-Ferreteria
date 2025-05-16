using ApiRestFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiRestFerreteria.Data
{
    public class DetalleVentaRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ApiResponse<List<Models.ListarDetalleVenta>> getDetallesVenta(int idFactura)
        {
            try
            {
                List<Models.ListarDetalleVenta> listarDetalleVentas = new List<Models.ListarDetalleVenta>();

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_ListarDetalleVentas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listarDetalleVentas.Add(new Models.ListarDetalleVenta
                            {
                                IdDetalleVenta = reader.GetInt32(0),
                                IdFactura = reader.GetInt16(1),
                                IdArticulo = reader.GetInt16(2),
                                Cantidad = reader.GetInt16(3),
                                NombreArticulo = reader.GetString(4),
                                PrecioUnitario = reader.GetDecimal(5),
                                FechaFactura = reader.GetDateTime(6),
                                NombreCliente = reader.GetString(7)
                            });
                        }
                    }
                }

                if (listarDetalleVentas.Count == 0)
                {
                    return new ApiResponse<List<Models.ListarDetalleVenta>>
                    {
                        StatusCode = 404,
                        Message = "No se encontraron detalles de venta para la factura especificada.",
                        data = null
                    };
                }

                return new ApiResponse<List<Models.ListarDetalleVenta>>
                {
                    StatusCode = 200,
                    Message = "Detalles de venta obtenidos correctamente.",
                    data = listarDetalleVentas
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<List<Models.ListarDetalleVenta>>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Models.ListarDetalleVenta>>
                {
                    StatusCode = 500,
                    Message = "Error inesperado: " + ex.Message,
                    data = null
                };
            }
        }

        public ApiResponse<Models.ListarDetalleVenta> getDetalleVenta(int idDetalleVenta)
        {
            try
            {
                Models.ListarDetalleVenta detalleVenta = new Models.ListarDetalleVenta();

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_BuscarDetalleVentaPorId", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDetalleVenta", idDetalleVenta);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detalleVenta.IdDetalleVenta = reader.GetInt32(0);
                            detalleVenta.IdFactura = reader.GetInt16(1);
                            detalleVenta.IdArticulo = reader.GetInt16(2);
                            detalleVenta.Cantidad = reader.GetInt16(3);
                            detalleVenta.NombreArticulo = reader.GetString(4);
                            detalleVenta.PrecioUnitario = reader.GetDecimal(5);
                            detalleVenta.FechaFactura = reader.GetDateTime(6);
                            detalleVenta.NombreCliente = reader.GetString(7);
                        }
                    }
                }

                if (detalleVenta.IdDetalleVenta == 0)
                {
                    return new ApiResponse<Models.ListarDetalleVenta>
                    {
                        StatusCode = 404,
                        Message = "No se encontró el detalle de venta especificado.",
                        data = null
                    };
                }

                return new ApiResponse<Models.ListarDetalleVenta>
                {
                    StatusCode = 200,
                    Message = "Detalle de venta obtenido correctamente.",
                    data = detalleVenta
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<Models.ListarDetalleVenta>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Models.ListarDetalleVenta>
                {
                    StatusCode = 500,
                    Message = "Error inesperado: " + ex.Message,
                    data = null
                };
            }
        }

        public ApiResponse<Models.DetalleVentaDB> insertarDetalleVenta(int idFactura, int idArticulo, int cantidad)
        {
            try
            {
                Models.DetalleVentaDB detalleVenta = new Models.DetalleVentaDB();

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_InsertarDetalleVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);
                    cmd.Parameters.AddWithValue("@IdArticulo", idArticulo);
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detalleVenta.IdDetalleVenta = reader.GetInt32(0);
                            detalleVenta.IdFactura = reader.GetInt16(1);
                            detalleVenta.IdArticulo = reader.GetInt16(2);
                            detalleVenta.Cantidad = reader.GetInt16(3);
                            detalleVenta.NombreArticulo = reader.GetString(4);
                            detalleVenta.Stock = reader.GetInt16(5);
                        }
                    }
                }

                if (detalleVenta.IdDetalleVenta == 0)
                {
                    return new ApiResponse<Models.DetalleVentaDB>
                    {
                        StatusCode = 404,
                        Message = "No se pudo insertar el detalle de venta.",
                        data = null
                    };
                }

                return new ApiResponse<Models.DetalleVentaDB>
                {
                    StatusCode = 200,
                    Message = "Detalle de venta insertado correctamente.",
                    data = detalleVenta
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<Models.DetalleVentaDB>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = null
                };
            }
            catch (Exception)
            {
                return new ApiResponse<Models.DetalleVentaDB>
                {
                    StatusCode = 500,
                    Message = "Error inesperado al insertar el detalle de venta.",
                    data = null
                };
            }
        }

        public ApiResponse<Models.DetalleVentaDB> actualizarDetalle(int idDetalleVenta, int idArticulo, int cantidad)
        {
            try
            {
                Models.DetalleVentaDB detalleVenta = new Models.DetalleVentaDB();

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_ActualizarDetalleVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDetalleVenta", idDetalleVenta);
                    cmd.Parameters.AddWithValue("@IdArticulo", idArticulo);
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detalleVenta.IdDetalleVenta = reader.GetInt32(0);
                            detalleVenta.IdFactura = reader.GetInt16(1);
                            detalleVenta.IdArticulo = reader.GetInt16(2);
                            detalleVenta.Cantidad = reader.GetInt16(3);
                            detalleVenta.NombreArticulo = reader.GetString(4);
                            detalleVenta.Stock = reader.GetInt16(5);
                        }
                    }
                }

                if (detalleVenta.IdDetalleVenta == 0)
                {
                    return new ApiResponse<Models.DetalleVentaDB>
                    {
                        StatusCode = 404,
                        Message = "No se pudo actualizar el detalle de venta.",
                        data = null
                    };
                }

                return new ApiResponse<Models.DetalleVentaDB>
                {
                    StatusCode = 200,
                    Message = "Detalle de venta actualizado correctamente.",
                    data = detalleVenta
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<Models.DetalleVentaDB>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = null
                };
            }
            catch (Exception)
            {
                return new ApiResponse<Models.DetalleVentaDB>
                {
                    StatusCode = 500,
                    Message = "Error inesperado al actualizar el detalle de venta.",
                    data = null
                };
            }
        }

        public ApiResponse<bool> eliminarDetalle(int idDetalle)
        {
            try
            {
                using(var conn = new SqlConnection(_connectionString))
                using(var cmd = new SqlCommand("sp_EliminarDetalleVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDetalleVenta", idDetalle);
                    conn.Open();

                    var result = cmd.ExecuteNonQuery();

                    if(result == 0)
                    {
                        return new ApiResponse<bool>
                        {
                            StatusCode = 404,
                            Message = "No se pudo eliminar el detalle de venta.",
                            data = false
                        };
                    }

                    return new ApiResponse<bool>
                    {
                        StatusCode = 200,
                        Message = "Detalle de venta eliminado correctamente.",
                        data = true
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + sqlEx.Message,
                    data = false
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 500,
                    Message = "Error inesperado: " + ex.Message,
                    data = false
                };
            }
        }

    }
}