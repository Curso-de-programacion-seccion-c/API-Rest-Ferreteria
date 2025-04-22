using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ApiRestFerreteria.Models;
using System.Web.Http.Results;

namespace ApiRestFerreteria.Articulo
{
    public class csArticulo
    {
        private string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public csEstructuraArticulo.responseArticulo insertarArticulo( int codeArticulo,
     string nombreArticulo, decimal precio, int stock, string descripcion, int idCategoria, int idProveedor)
        {
            var result = new csEstructuraArticulo.responseArticulo();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("InsertarArticulo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodeArticulo", codeArticulo);
                    cmd.Parameters.AddWithValue("@NombreArticulo", nombreArticulo);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);

                    con.Open();
                    var resultId = cmd.ExecuteScalar();

                    result.respuesta = resultId != null ? Convert.ToInt32(resultId) : 0;
                    result.descripcion_respuesta = resultId != null ? "Artículo creado exitosamente" : "Error al insertar el artículo";
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Ocurrió un error: " + ex.Message;
                }
            }
            return result;
        }



        public csEstructuraArticulo.responseArticulo obtenerArticulos()
        {
            var result = new csEstructuraArticulo.responseArticulo();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Articulos", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    result.respuesta = dt.Rows.Count;
                    result.descripcion_respuesta = "Artículos obtenidos exitosamente";
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Ocurrió un error: " + ex.Message;
                }
            }
            return result;
        }

        //Captura lo que el sp devuelve como resultado explicito
        public csEstructuraArticulo.responseArticulo actualizarArticulo(
        int idArticulo,int codeArticulo, string nombreArticulo,decimal precio, string descripcion,int stock, int idCategoria,int idProveedor)
        {
            var result = new csEstructuraArticulo.responseArticulo();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ActualizarArticulo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdArticulo", idArticulo);
                    cmd.Parameters.AddWithValue("@CodeArticulo", codeArticulo);
                    cmd.Parameters.AddWithValue("@NombreArticulo", nombreArticulo);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.respuesta = Convert.ToInt32(reader["Resultado"]);
                            result.descripcion_respuesta = reader["Mensaje"].ToString();
                        }
                        else
                        {
                            result.respuesta = 0;
                            result.descripcion_respuesta = "No se obtuvo respuesta del procedimiento.";
                        }
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


        public csEstructuraArticulo.responseArticulo eliminarArticulo(int idArticulo)
        {
            var result = new csEstructuraArticulo.responseArticulo();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("EliminarArticulo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdArticulo", idArticulo); 

                    con.Open();
                    cmd.ExecuteNonQuery();

                    result.respuesta = 1;
                    result.descripcion_respuesta = "Artículo eliminado exitosamente";
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Ocurrió un error: " + ex.Message;
                }
            }
            return result;
        }
        public csEstructuraArticulo.articulo obtenerArticuloPorId(int idArticulo)
        {
            var art = new csEstructuraArticulo.articulo();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ObtenerArticuloPorId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdArticulo", idArticulo);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        art.IdArticulo = Convert.ToInt32(reader["IdArticulo"]);
                        art.CodeArticulo = Convert.ToInt32(reader["CodeArticulo"]); 
                        art.NombreArticulo = reader["Nombre"].ToString();
                        art.Precio = Convert.ToDecimal(reader["PrecioUnitario"]);
                        art.Stock = Convert.ToInt32(reader["Stock"]);
                        art.Descripcion = reader["Descripcion"].ToString();
                    }
                }
                catch (Exception ex)
                {
              
                }
            }

            return art;
        }

    }
}
