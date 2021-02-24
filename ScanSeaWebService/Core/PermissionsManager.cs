using ScanSeaProtocols.Core;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ScanSeaWebService.Core
{
    public class PermissionsManager
    {

        public static List<int> GetPermissions(string code)
        {
            SqlConnection connection = DatabaseManager.Instance.Connection;
            connection.Open();

            SqlCommand cmd = new SqlCommand("GetPermissions", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Code", code));

            SqlDataReader reader = cmd.ExecuteReader();

            List<int> permissions = new List<int>();

            while (reader.Read())
            {
                permissions.Add(reader.GetInt32(0));
            }

            reader.Close();
            connection.Close();

            return permissions;
        }

        public static List<Permission> GetPermissionsEnum(string code)
        {
            SqlConnection connection = DatabaseManager.Instance.Connection;
            connection.Open();

            SqlCommand cmd = new SqlCommand("GetPermissions", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Code", code));

            SqlDataReader reader = cmd.ExecuteReader();

            List<Permission> permissions = new List<Permission>();

            while (reader.Read())
            {
                permissions.Add((Permission)reader.GetInt32(0));
            }

            reader.Close();
            connection.Close();

            return permissions;
        }

    }
}
