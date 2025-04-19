using ApiRestFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                if(formasPagoDBs.Count == 0) 
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


    }
}