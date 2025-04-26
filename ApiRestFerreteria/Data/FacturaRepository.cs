using ApiRestFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Data
{
    public class FacturaRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ApiResponse<List<Models.ListarFacturas>> getFacturas()
        {
            List<Models.ListarFacturas> facturas = new List<Models.ListarFacturas>();
            try
            {
                using(var conn = new SqlConnection(_connectionString))
                using(var cmd = new SqlCommand("sp_ListarFacturas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Models.ListarFacturas factura = new Models.ListarFacturas();
                                factura.idFactura = reader.GetInt16(0);
                                factura.fecha = reader.GetDateTime(1);
                                factura.Total_Pago = reader.GetDecimal(2);
                                factura.idEmpleado = reader.GetInt16(3);
                                factura.nombreEmpleado = reader.GetString(4);
                                factura.idCliente = reader.GetInt16(5);
                                factura.nombreCliente = reader.GetString(6);
                                factura.idFormaPago = reader.GetByte(7);
                                factura.nombreFormaPago = reader.GetString(8);

                                facturas.Add(factura);
                            }
                        }
                    }
                }

                if(facturas.Count == 0)
                {
                    return new ApiResponse<List<ListarFacturas>>
                    {
                        StatusCode = 404,
                        Message = "No se encontraron facturas",
                        data = null
                    };
                }

                return new ApiResponse<List<ListarFacturas>>
                {
                    StatusCode = 200,
                    Message = "Facturas obtenidas",
                    data = facturas
                };
            }
            catch (SqlException sqlEX)
            {
                return new ApiResponse<List<ListarFacturas>>
                {
                    StatusCode = 500,
                    Message = sqlEX.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ListarFacturas>>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    data = null
                };
            }
        }

        public ApiResponse<Models.ListarFacturas> getFactura(int idFactura)
        {
            try
            {
                Models.ListarFacturas listarFactura = new Models.ListarFacturas();
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_BuscarFacturaPorId", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);
                    conn.Open();

                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            listarFactura.idFactura = reader.GetInt16(0);
                            listarFactura.fecha = reader.GetDateTime(1);
                            listarFactura.Total_Pago = reader.GetDecimal(2);
                            listarFactura.idEmpleado = reader.GetInt16(3);
                            listarFactura.nombreEmpleado = reader.GetString(4);
                            listarFactura.idCliente = reader.GetInt16(5);
                            listarFactura.nombreCliente = reader.GetString(6);
                            listarFactura.idFormaPago = reader.GetByte(7);
                            listarFactura.nombreFormaPago = reader.GetString(8);
                        }
                    }
                }

                if (listarFactura.idFactura == 0)
                {
                    return new ApiResponse<Models.ListarFacturas>
                    {
                        StatusCode = 404,
                        Message = "No se encontró la factura",
                        data = null
                    };
                }

                return new ApiResponse<Models.ListarFacturas>
                {
                    StatusCode = 200,
                    Message = "Factura obtenida",
                    data = listarFactura
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<Models.ListarFacturas>
                {
                    StatusCode = 500,
                    Message = sqlEx.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Models.ListarFacturas>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    data = null
                };
            }
        }

        public ApiResponse<Models.Factura> crearFactura(Models.Factura factura)
        {
            Models.Factura facturaResponse = new Models.Factura();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_InsertarFactura", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEmpleado", factura.idEmpleado);
                    cmd.Parameters.AddWithValue("@IdCliente", factura.idCliente);
                    cmd.Parameters.AddWithValue("@Fecha", factura.fecha);
                    cmd.Parameters.AddWithValue("@IdFormaPago ", factura.idFormaPago);

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            facturaResponse.idFactura = reader.GetInt16(0);
                            facturaResponse.idEmpleado = reader.GetInt16(1);
                            facturaResponse.idCliente = reader.GetInt16(2);
                            facturaResponse.fecha = reader.GetDateTime(3);
                            facturaResponse.Total_Pago = reader.GetDecimal(4);
                            facturaResponse.idFormaPago = reader.GetByte(5);
                        }
                    }
                }

                if (facturaResponse.idFactura == 0)
                {
                    return new ApiResponse<Models.Factura>
                    {
                        StatusCode = 404,
                        Message = "No se pudo crear la factura",
                        data = null
                    };
                }

                return new ApiResponse<Models.Factura>
                {
                    StatusCode = 200,
                    Message = "Factura creada",
                    data = facturaResponse
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<Models.Factura>
                {
                    StatusCode = 500,
                    Message = sqlEx.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Models.Factura>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    data = null
                };
            }
        }

        public ApiResponse<Models.Factura> actualizarFactura(Models.Factura factura)
        {
            Models.Factura facturaResponse = new Models.Factura();

            try
            {
                using(var conn = new SqlConnection(_connectionString))
                using(var cmd =  new SqlCommand("sp_ActualizarFactura", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFactura", factura.idFactura);
                    cmd.Parameters.AddWithValue("@IdEmpleado", factura.idEmpleado);
                    cmd.Parameters.AddWithValue("@IdCliente", factura.idCliente);
                    cmd.Parameters.AddWithValue("@Fecha", factura.fecha);
                    cmd.Parameters.AddWithValue("@IdFormaPago", factura.idFormaPago);

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            facturaResponse.idFactura = reader.GetInt16(0);
                            facturaResponse.idEmpleado = reader.GetInt16(1);
                            facturaResponse.idCliente = reader.GetInt16(2);
                            facturaResponse.fecha = reader.GetDateTime(3);
                            facturaResponse.Total_Pago = reader.GetDecimal(4);
                            facturaResponse.idFormaPago = reader.GetByte(5);
                        }
                    }

                    if(facturaResponse.idFactura == 0)
                    {
                        return new ApiResponse<Models.Factura>
                        {
                            StatusCode = 404,
                            Message = "No se pudo actualizar la factura",
                            data = null
                        };
                    }

                    return new ApiResponse<Models.Factura>
                    {
                        StatusCode = 200,
                        Message = "Factura actualizada",
                        data = facturaResponse
                    };
                }
            }
            catch (SqlException ex)
            {
                return new ApiResponse<Models.Factura>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Models.Factura>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    data = null
                };
            }
        }
        
        public ApiResponse<bool> eliminarFactura(int idFactura)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_EliminarFactura", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return new ApiResponse<bool>
                        {
                            StatusCode = 404,
                            Message = "No se pudo eliminar la factura",
                            data = false
                        };
                    }
                    else
                    {
                        return new ApiResponse<bool>
                        {
                            StatusCode = 200,
                            Message = "Factura eliminada",
                            data = true
                        };
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 500,
                    Message = sqlEx.Message,
                    data = false
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    data = false
                };
            }
        }
        
    }
}