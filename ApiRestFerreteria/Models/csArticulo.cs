using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ApiRestFerreteria.Models;
using System.Web.Http.Results;
using System.Collections.Generic;
using static ApiRestFerreteria.Articulo.csEstructuraArticulo;

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



        public List<ArticulosModel> obtenerArticulos()
        {
            List<ArticulosModel> articulos = new List<ArticulosModel>(); //Esto crea una lista para guardar los articulos de la bd
            string connectionString = "Data Source=localhost;Initial Catalog=FerreteriaDB;Integrated Security=True;"; //Cambiar el Data Source por la direccion de la maquina

            using (SqlConnection conn = new SqlConnection(connectionString))//Conexion a la base de datos
            {
                SqlCommand cmd = new SqlCommand("ObtenerArticulos", conn); //Reemplazar Obtener Articulos por el nombre del sp
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) //El ciclo lee los datos que envio el sp fila por fila
                {
                    articulos.Add(new ArticulosModel //Por cada fila del resultado, se crea un objeto y lo añade a la lista
                    {
                        IdArticulo = Convert.ToInt32(reader["IdArticulo"]),
                        CodeArticulo = Convert.ToInt32(reader["CodeArticulo"]),
                        NombreArticulo = reader["NombreArticulo"].ToString(),
                        Precio = Convert.ToDecimal(reader["Precio"]),
                        Stock = Convert.ToInt32(reader["Stock"]),
                        Descripcion = reader["Descripcion"].ToString(),
                        IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                        IdProveedor = Convert.ToInt32(reader["IdProveedor"])
                    });
                }
            }

            return articulos;
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
