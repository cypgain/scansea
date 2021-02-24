using System.Data;
using System.Data.SqlClient;

namespace ScanSeaWebService.Core
{
    public class DatabaseManager
    {

        public static readonly DatabaseManager Instance = new DatabaseManager();

        public SqlConnection Connection { get; private set; }

        private DatabaseManager()
        {
            string connectionString = new SqlConnectionStringBuilder()
            {
                DataSource = @"DESKTOP-EFQ33SM\TOM",
                InitialCatalog = @"ScanSea",
                UserID = @"sa",
                Password = @"root",
                TrustServerCertificate = true
            }.ConnectionString;

            Connection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnectionForCode(string code)
        {
            int idSociete;
            string connectionString;

            Connection.Open();

            // Recuperation de l'id de la societe associe au code
            SqlCommand cmd = new SqlCommand("GetIdSociete", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Code", code));

            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            idSociete = reader.GetInt32(0);
            reader.Close();


            // Recuperation de la chaine de connection de la societe
            cmd = new SqlCommand("GetConnectionString", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IdSociete", idSociete));

            reader = cmd.ExecuteReader();

            reader.Read();
            connectionString = reader.GetString(0);
            reader.Close();

            Connection.Close();

            return new SqlConnection(connectionString);
        }

    }
}
