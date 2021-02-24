using Microsoft.AspNetCore.Mvc;
using ScanSeaProtocols;
using ScanSeaProtocols.Messages;
using ScanSeaWebService.Core;
using System.Data;
using System.Data.SqlClient;

namespace ScanSeaWebService
{
    [Route("[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {

        [HttpGet("{code}")]
        public string GetUtilisateur(string code)
        {
            if (string.IsNullOrEmpty(code))
                return new CodeMessageData(MessageResponse.ERROR).ToJson();


            SqlConnection connection = DatabaseManager.Instance.Connection;
            connection.Open();

            SqlCommand cmd = new SqlCommand("GetCodes", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Code", code));

            SqlDataReader reader = cmd.ExecuteReader();

            bool hasRows = reader.HasRows;

            int idSociete = -1;
            int idAgence = -1;

            if (hasRows)
            {
                reader.Read();
                idSociete = reader.GetInt32(1);
                idAgence = reader.GetInt32(2);
            }

            reader.Close();
            connection.Close();

            return hasRows ? new CodeMessageData(MessageResponse.OK, idSociete, idAgence).ToJson() : new CodeMessageData(MessageResponse.ERROR).ToJson();
        }
    }
}
