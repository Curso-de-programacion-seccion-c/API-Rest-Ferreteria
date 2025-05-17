using ApiRestFerreteria.Models;
using ApiRestFerreteria.Models.ReportesFacturas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Data
{
    public class ReporteRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ApiResponse<List<Models.ReportesFacturas.ReporteFacturaDetalle>> getFacturaById(int idFactura)
        {
            try
            {
                List<Models.ReportesFacturas.ReporteFacturaDetalle> facturasDetalle = new List<Models.ReportesFacturas.ReporteFacturaDetalle>();
                using(var conn = new SqlConnection(_connectionString))
                using(var cmd = new SqlCommand("sp_ReporteFacturaPorId", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.ReportesFacturas.ReporteFacturaDetalle factura = new Models.ReportesFacturas.ReporteFacturaDetalle();
                            factura.IdFactura = reader.GetInt16(0);
                            factura.Fecha = reader.GetDateTime(1);
                            factura.Total_Pago = reader.GetDecimal(2);
                            factura.IdCliente = reader.GetInt16(3);
                            factura.NombreCliente = reader.GetString(4);
                            factura.telefono = reader.GetString(5);
                            factura.NitCliente = reader.GetString(6);
                            factura.DpiCliente = reader.GetString(7);
                            factura.IdEmpleado = reader.GetInt16(8);
                            factura.NombreEmpleado = reader.GetString(9);
                            factura.DpiEmpleado = reader.GetString(10);
                            factura.Puesto = reader.GetString(11);
                            factura.EmailEmpleado = reader.GetString(12);
                            factura.RolDelEmpleado = reader.GetString(13);
                            factura.NombreFormaPago = reader.GetString(14);
                            factura.IdDetalleVenta = reader.GetInt32(15);
                            factura.IdArticulo = reader.GetInt16(16);
                            factura.NombreArticulo = reader.GetString(17);
                            factura.PrecioUnitario = reader.GetDecimal(18);
                            factura.Cantidad = reader.GetInt16(19);
                            factura.Subtotal = reader.GetDecimal(20);
                            factura.PrecioSinIVA = reader.GetDecimal(21);
                            factura.IVA = reader.GetDecimal(22);


                            facturasDetalle.Add(factura);
                        }
                    }
                }

                if(facturasDetalle.Count == 0)
                {
                    return new ApiResponse<List<Models.ReportesFacturas.ReporteFacturaDetalle>>()
                    {
                        StatusCode = 404,
                        Message = "No se encontraron resultados",
                        data = null
                    };
                }

                return new ApiResponse<List<Models.ReportesFacturas.ReporteFacturaDetalle>>()
                {
                    StatusCode = 200,
                    Message = "Consulta exitosa",
                    data = facturasDetalle
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<List<Models.ReportesFacturas.ReporteFacturaDetalle>>()
                {
                    StatusCode = 500,
                    Message = sqlEx.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Models.ReportesFacturas.ReporteFacturaDetalle>>()
                {
                    StatusCode = 500,
                    Message = "Error interno del servidor: " + ex.Message,
                    data = null
                };
            }
        }

        public ApiResponse<List<ResponseReporteFecha>> getFacturaByFecha(Models.ReportesFacturas.RequestReporteFecha requestReporteFecha)
        {
            try
            {
                List<ResponseReporteFecha> responseReporteFechas = new List<ResponseReporteFecha>();
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_ReporteVentasPorFechas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@fechaInicio", requestReporteFecha.fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", requestReporteFecha.fechaFin);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResponseReporteFecha response = new ResponseReporteFecha();
                            response.IdFactura = reader.GetInt16(0);
                            response.Fecha = reader.GetDateTime(1);
                            response.Total_Pago = reader.GetDecimal(2);
                            response.IdCliente = reader.GetInt16(3);
                            response.NombreCliente = reader.GetString(4);
                            response.telefono = reader.GetString(5);
                            response.NIT = reader.GetString(6);
                            response.IdEmpleado = reader.GetInt16(7);
                            response.NombreEmpleado = reader.GetString(8);
                            response.RolDelEmpleado = reader.GetString(9);
                            response.FormaPago = reader.GetString(10);

                            responseReporteFechas.Add(response);
                        }
                    }
                }

                if (responseReporteFechas.Count == 0)
                {
                    return new ApiResponse<List<ResponseReporteFecha>>()
                    {
                        StatusCode = 404,
                        Message = "No se encontraron resultados",
                        data = null
                    };
                }

                return new ApiResponse<List<ResponseReporteFecha>>()
                {
                    StatusCode = 200,
                    Message = "Consulta exitosa",
                    data = responseReporteFechas
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<List<ResponseReporteFecha>>()
                {
                    StatusCode = 500,
                    Message = sqlEx.Message,
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResponseReporteFecha>>()
                {
                    StatusCode = 500,
                    Message = "Error interno del servidor: " + ex.Message,
                    data = null
                };
            }
        }
    }
}