using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ApiRestFerreteria.Articulo
{
    public class csArticulo
    {
        private string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public csEstructuraArticulo.responseArticulo insertarArticulo(string nombreArticulo, decimal precio, int stock, int idCategoria, int idProveedor)
        {
            var result = new csEstructuraArticulo.responseArticulo();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("InsertarArticulo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreArticulo", nombreArticulo);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Stock", stock);
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

        public csEstructuraArticulo.responseArticulo actualizarArticulo(int idArticulo, string nombreArticulo, decimal precio, int stock)
        {
            var result = new csEstructuraArticulo.responseArticulo();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ActualizarArticulo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdArticulo", idArticulo);
                    cmd.Parameters.AddWithValue("@NombreArticulo", nombreArticulo);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Stock", stock);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    result.respuesta = rowsAffected > 0 ? 1 : 0;
                    result.descripcion_respuesta = rowsAffected > 0 ? "Artículo actualizado exitosamente" : "No se actualizó el artículo";
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
                    int rowsAffected = cmd.ExecuteNonQuery();

                    result.respuesta = rowsAffected > 0 ? 1 : 0;
                    result.descripcion_respuesta = rowsAffected > 0 ? "Artículo eliminado exitosamente" : "No se eliminó el artículo";
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
