using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Data
{
    public class RolesRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        public List<Models.Roles> GetRoles()
        {
            List<Models.Roles> roles = new List<Models.Roles>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SpListRoles", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Models.Roles
                            {
                                IdRol = reader.GetByte(0),
                                Nombre = reader.GetString(1),
                                Sueldo = reader.GetDecimal(2)
                            });
                        }
                    }
                }
            }

            return roles;
        }

    }
}