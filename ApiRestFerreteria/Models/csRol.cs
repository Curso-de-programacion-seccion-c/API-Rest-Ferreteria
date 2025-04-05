using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ApiRestFerreteria.Rol
{
    public class csRol
    {
        private readonly string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public csRol() { }

        // Insertar nuevo rol
        public csEstructuraRol.responseRol insertarRol(string Nombre, decimal Sueldo)
        {
            csEstructuraRol.responseRol result = new csEstructuraRol.responseRol();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("InsertarRol", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.Parameters.AddWithValue("@Sueldo", Sueldo);

                    var resultId = cmd.ExecuteScalar();

                    result.respuesta = resultId != null ? Convert.ToInt32(resultId) : 0;
                    result.descripcion_respuesta = resultId != null
                        ? "Rol insertado exitosamente"
                        : "No se pudo insertar el rol";
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Error: " + ex.Message;
                }
            }

            return result;
        }

        // Obtener todos los roles
        public List<csEstructuraRol.RolData> obtenerRoles()
        {
            List<csEstructuraRol.RolData> lista = new List<csEstructuraRol.RolData>();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT IdRol, Nombre, Sueldo FROM Roles", con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new csEstructuraRol.RolData
                    {
                        IdRol = Convert.ToInt32(reader["IdRol"]),
                        Nombre = reader["Nombre"].ToString(),
                        Sueldo = Convert.ToDecimal(reader["Sueldo"])
                    });
                }
            }

            return lista;
        }

        // Obtener un rol por su ID
        public csEstructuraRol.RolData obtenerRolPorId(int id)
        {
            csEstructuraRol.RolData rol = null;

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT IdRol, Nombre, Sueldo FROM Roles WHERE IdRol = @IdRol", con);
                cmd.Parameters.AddWithValue("@IdRol", id);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    rol = new csEstructuraRol.RolData
                    {
                        IdRol = Convert.ToInt32(reader["IdRol"]),
                        Nombre = reader["Nombre"].ToString(),
                        Sueldo = Convert.ToDecimal(reader["Sueldo"])
                    };
                }
            }

            return rol;
        }

        // Actualizar un rol existente
        public csEstructuraRol.responseRol actualizarRol(int id, string nombre, decimal sueldo)
        {
            csEstructuraRol.responseRol result = new csEstructuraRol.responseRol();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Roles SET Nombre = @Nombre, Sueldo = @Sueldo WHERE IdRol = @IdRol", con);
                    cmd.Parameters.AddWithValue("@IdRol", id);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Sueldo", sueldo);
                    con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    result.respuesta = rowsAffected;
                    result.descripcion_respuesta = rowsAffected > 0
                        ? "Rol actualizado correctamente"
                        : "No se encontró el rol para actualizar";
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Error: " + ex.Message;
                }
            }

            return result;
        }

        // Eliminar un rol
        public csEstructuraRol.responseRol eliminarRol(int id)
        {
            csEstructuraRol.responseRol result = new csEstructuraRol.responseRol();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Roles WHERE IdRol = @IdRol", con);
                    cmd.Parameters.AddWithValue("@IdRol", id);
                    con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    result.respuesta = rowsAffected;
                    result.descripcion_respuesta = rowsAffected > 0
                        ? "Rol eliminado correctamente"
                        : "No se encontró el rol para eliminar";
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Error: " + ex.Message;
                }
            }

            return result;
        }
    }
}
