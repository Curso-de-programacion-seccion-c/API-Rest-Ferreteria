using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ApiRestFerreteria.Cliente
{
    public class csCliente
    {
        private readonly string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public csCliente() { }

        public csEstructuraCliente.ResponseCliente InsertarCliente(string dpi, string nombre, string apellido, string nit, string correoElectronico, string telefono, DateTime fechaRegistro)
        {
            csEstructuraCliente.ResponseCliente result = new csEstructuraCliente.ResponseCliente();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CreateCliente", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@Dpi", dpi);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);
                    cmd.Parameters.AddWithValue("@NIT", nit);
                    cmd.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@FechaRegistro", fechaRegistro);

                    var resultId = cmd.ExecuteScalar();

                    result.Respuesta = resultId != null ? Convert.ToInt32(resultId) : 0;
                    result.DescripcionRespuesta = resultId != null
                        ? "Cliente insertado exitosamente"
                        : "No se pudo insertar el cliente";
                }
                catch (Exception ex)
                {
                    result.Respuesta = 0;
                    result.DescripcionRespuesta = "Error: " + ex.Message;
                }
            }

            return result;
        }

        public List<csEstructuraCliente.ClienteData> ObtenerClientes()
        {
            List<csEstructuraCliente.ClienteData> lista = new List<csEstructuraCliente.ClienteData>();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT IdCliente, Dpi, Nombre, Apellido, NIT, CorreoElectronico, Telefono, FechaRegistro FROM Clientes", con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new csEstructuraCliente.ClienteData
                    {
                        IdCliente = Convert.ToInt32(reader["IdCliente"]),
                        Dpi = reader["Dpi"].ToString(),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        NIT = reader["NIT"].ToString(),
                        CorreoElectronico = reader["CorreoElectronico"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                    });
                }
            }

            return lista;
        }

        public csEstructuraCliente.ClienteData ObtenerClientePorId(int id)
        {
            csEstructuraCliente.ClienteData cliente = null;

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT IdCliente, Dpi, Nombre, Apellido, NIT, CorreoElectronico, Telefono, FechaRegistro FROM Clientes WHERE IdCliente = @IdCliente", con);
                cmd.Parameters.AddWithValue("@IdCliente", id);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cliente = new csEstructuraCliente.ClienteData
                    {
                        IdCliente = Convert.ToInt32(reader["IdCliente"]),
                        Dpi = reader["Dpi"].ToString(),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        NIT = reader["NIT"].ToString(),
                        CorreoElectronico = reader["CorreoElectronico"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                    };
                }
            }

            return cliente;
        }


        public csEstructuraCliente.ResponseCliente ActualizarCliente(int id, string dpi, string nombre, string apellido, string nit, string correoElectronico, string telefono, DateTime fechaRegistro)
        {
            csEstructuraCliente.ResponseCliente result = new csEstructuraCliente.ResponseCliente();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Clientes SET Dpi = @Dpi, Nombre = @Nombre, Apellido = @Apellido, NIT = @NIT, CorreoElectronico = @CorreoElectronico, Telefono = @Telefono, FechaRegistro = @FechaRegistro WHERE IdCliente = @IdCliente", con);
                    cmd.Parameters.AddWithValue("@IdCliente", id);
                    cmd.Parameters.AddWithValue("@Dpi", dpi);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);
                    cmd.Parameters.AddWithValue("@NIT", nit);
                    cmd.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@FechaRegistro", fechaRegistro);
                    con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    result.Respuesta = rowsAffected;
                    result.DescripcionRespuesta = rowsAffected > 0
                        ? "Cliente actualizado correctamente"
                        : "No se encontró el cliente para actualizar";
                }
                catch (Exception ex)
                {
                    result.Respuesta = 0;
                    result.DescripcionRespuesta = "Error: " + ex.Message;
                }
            }

            return result;
        }

        public csEstructuraCliente.ResponseCliente EliminarCliente(int id)
        {
            csEstructuraCliente.ResponseCliente result = new csEstructuraCliente.ResponseCliente();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Clientes WHERE IdCliente = @IdCliente", con);
                    cmd.Parameters.AddWithValue("@IdCliente", id);
                    con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    result.Respuesta = rowsAffected;
                    result.DescripcionRespuesta = rowsAffected > 0
                        ? "Cliente eliminado correctamente"
                        : "No se encontró el cliente para eliminar";
                }
                catch (Exception ex)
                {
                    result.Respuesta = 0;
                    result.DescripcionRespuesta = "Error: " + ex.Message;
                }
            }

            return result;
        }
    }
}