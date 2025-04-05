using ApiRestFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Data
{
    public class CategoriaRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        public ApiResponse<List<Models.Categoria>> GetCategorias()
        {
            try
            {
                List<Models.Categoria> categorias = new List<Models.Categoria>();

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SpListarCategorias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                categorias.Add(new Models.Categoria
                                {
                                    idCategoria = reader.GetByte(0),
                                    nombreCategoria = reader.GetString(1),
                                });
                            }
                        }
                    }
                }

                return new ApiResponse<List<Models.Categoria>>
                {
                    StatusCode = 200,
                    Message = "Categorias obtenidas",
                    data = categorias
                };
            }
            catch (SqlException sqlEx)
            {
                return new ApiResponse<List<Models.Categoria>>
                {
                    StatusCode = 400,
                    Message = sqlEx.Message,
                    data = null
                };
            }
        }

        // obtener categoria por id
        public ApiResponse<Models.Categoria> GetCategoria(Models.Categoria categoria)
        {
            try
            {
                Models.Categoria modelCategoria = new Models.Categoria();

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SpBuscarCategoria", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCategoria", categoria.idCategoria);
                    cmd.Parameters.AddWithValue("@nombre", categoria.nombreCategoria);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                modelCategoria.idCategoria = reader.GetByte(0);
                                modelCategoria.nombreCategoria = reader.GetString(1);
                            }
                        }
                    }
                }

                if (modelCategoria.idCategoria == 0)
                {
                    return new ApiResponse<Models.Categoria>
                    {
                        StatusCode = 400,
                        Message = "No se encontro la categoria",
                        data = null
                    };
                }

                return new ApiResponse<Models.Categoria>
                {
                    StatusCode = 200,
                    Message = "Categoria encontrada",
                    data = modelCategoria
                };
            }
            catch (SqlException sqlex)
            {
                return new ApiResponse<Models.Categoria>
                {
                    StatusCode = 400,
                    Message = sqlex.Message,
                    data = null
                };
            }
        }

        public ApiResponse<string> CrearCategoria(Models.Categoria categoria)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SpCrearCategoria", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreCategoria", categoria.nombreCategoria);
                    conn.Open();
                    int rowsAffect = cmd.ExecuteNonQuery();

                    if (rowsAffect == 0)
                    {
                        return new ApiResponse<string>
                        {
                            StatusCode = 400,
                            Message = "No se pudo crear la categoria",
                            data = null
                        };
                    }

                    return new ApiResponse<string>
                    {
                        StatusCode = 200,
                        Message = "Categoria creada exitosamente",
                        data = null
                    };
                }
            }
            catch (Exception ex)
            {
                if (ex is SqlException sqlEx)
                {
                    return new ApiResponse<string>
                    {
                        StatusCode = 400,
                        Message = sqlEx.Message,
                        data = null
                    };
                }
                else
                {
                    return new ApiResponse<string>
                    {
                        StatusCode = 500,
                        Message = "Error al crear la categoria: " + ex.Message,
                        data = null
                    };
                }
            }
        }

        public ApiResponse<string> ActualizarCategoria(Models.Categoria categoria)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SpActualizarCategoria", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCategoria", categoria.idCategoria);
                    cmd.Parameters.AddWithValue("@nombre", categoria.nombreCategoria);
                    conn.Open();

                    int rowsAffect = cmd.ExecuteNonQuery();

                    if (rowsAffect == 0)
                    {
                        return new ApiResponse<string>
                        {
                            StatusCode = 400,
                            Message = "No se pudo actualizar la categoria",
                            data = null
                        };
                    }

                    return new ApiResponse<string>
                    {
                        StatusCode = 200,
                        Message = "Categoria actualizada exitosamente",
                        data = null
                    };
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
        }

        public ApiResponse<string> EliminarCategoria(int idCategoria)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SpEliminarCategoria", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                    conn.Open();

                    int rowsAffect = cmd.ExecuteNonQuery();

                    if (rowsAffect == 0)
                    {
                        return new ApiResponse<string>
                        {
                            StatusCode = 400,
                            Message = "No se pudo eliminar la categoria",
                            data = null
                        };
                    }

                    return new ApiResponse<string>
                    {
                        StatusCode = 200,
                        Message = "Categoria eliminada exitosamente",
                        data = null
                    };
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
        }
    }
}