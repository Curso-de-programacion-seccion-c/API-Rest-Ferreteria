using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ApiRestFerreteria.Proveedor
{
    public class csProveedor
    {
        private string conexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Crear un nuevo proveedor
        public csEstructuraProveedor.responseProveedor insertarProveedor(string nombreProveedor, string telefono, string nombreContacto)
        {
            csEstructuraProveedor.responseProveedor result = new csEstructuraProveedor.responseProveedor();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("InsertarProveedor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreProveedor", nombreProveedor);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@NombreContacto", nombreContacto);

                    con.Open();
                    var resultId = cmd.ExecuteScalar();

                    if (resultId != null)
                    {
                        result.respuesta = Convert.ToInt32(resultId);
                        result.descripcion_respuesta = "Proveedor creado exitosamente";
                    }
                    else
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = "Error al insertar el proveedor";
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

        // Obtener todos los proveedores
        public csEstructuraProveedor.responseProveedor obtenerProveedores()
        {
            csEstructuraProveedor.responseProveedor result = new csEstructuraProveedor.responseProveedor();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Proveedor WHERE IsActive = 1", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    result.respuesta = dt.Rows.Count;
                    result.descripcion_respuesta = "Proveedores obtenidos exitosamente";
                  
                }
                catch (Exception ex)
                {
                    result.respuesta = 0;
                    result.descripcion_respuesta = "Ocurrió un error: " + ex.Message;
                }
            }
            return result;
        }

        // Actualizar un proveedor
        public csEstructuraProveedor.responseProveedor actualizarProveedor(int idProveedor, string nombreProveedor, string telefono, string nombreContacto)
        {
            csEstructuraProveedor.responseProveedor result = new csEstructuraProveedor.responseProveedor();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ActualizarProveedor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@NombreProveedor", nombreProveedor);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@NombreContacto", nombreContacto);

                    con.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.respuesta = 1;
                        result.descripcion_respuesta = "Proveedor actualizado exitosamente";
                    }
                    else
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = "No se encontró el proveedor o no se actualizó";
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

        // Eliminar un proveedor
        public csEstructuraProveedor.responseProveedor eliminarProveedor(int idProveedor)
        {
            csEstructuraProveedor.responseProveedor result = new csEstructuraProveedor.responseProveedor();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("EliminarProveedor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);

                    con.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.respuesta = 1;
                        result.descripcion_respuesta = "Proveedor eliminado exitosamente";
                    }
                    else
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = "No se encontró el proveedor o no se eliminó";
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

        // Activar o desactivar proveedor
        public csEstructuraProveedor.responseProveedor activarDesactivarProveedor(int idProveedor, bool isActive)
        {
            csEstructuraProveedor.responseProveedor result = new csEstructuraProveedor.responseProveedor();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ActualizarProveedorIsActive", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);

                    con.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.respuesta = 1;
                        result.descripcion_respuesta = isActive ? "Proveedor activado exitosamente" : "Proveedor desactivado exitosamente";
                    }
                    else
                    {
                        result.respuesta = 0;
                        result.descripcion_respuesta = "No se encontró el proveedor o no se actualizó";
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
    }
}