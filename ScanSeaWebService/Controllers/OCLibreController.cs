using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using ScanSeaProtocols;
using ScanSeaProtocols.Core;
using ScanSeaProtocols.Messages;
using ScanSeaWebService.Core;

namespace ScanSeaWebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OCLibreController : ControllerBase
    {

        [HttpGet("numero/{code}/{agence}/{numero}")]
        public string GetOCLibreNumero(string code, int agence, string numero)
        {
            List<Permission> permissions = PermissionsManager.GetPermissionsEnum(code);

            if (!(permissions.Contains(Permission.OC_LIBRE)))
                return new OCLibreNumeroMessageData(MessageResponse.ERROR).ToJson();

            SqlConnection con = DatabaseManager.Instance.GetConnectionForCode(code);
            con.Open();

            SqlCommand cmd = new SqlCommand("SCAN_OCLibre_Numero", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IdAgence", agence));
            cmd.Parameters.Add(new SqlParameter("@Numero", numero));

            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            int retIdRec = reader.GetInt32(0);
            int retIdCgmt = reader.GetInt32(1);

            reader.Close();
            con.Close();

            return new OCLibreNumeroMessageData(MessageResponse.OK, retIdRec, retIdCgmt).ToJson();
        }

        [HttpGet("palette/{code}/{agence}/{palette}/{idrec}/{idcgmt}")]
        public string GetOCLibrePalette(string code, int agence, string palette, int idrec, int idcgmt)
        {
            List<Permission> permissions = PermissionsManager.GetPermissionsEnum(code);

            if (!(permissions.Contains(Permission.OC_LIBRE)))
                return new OCLibrePaletteMessageData(MessageResponse.ERROR).ToJson();

            SqlConnection con = DatabaseManager.Instance.GetConnectionForCode(code);
            con.Open();

            SqlCommand cmd = new SqlCommand("SCAN_OCLibre_Palette", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IdAgence", agence));
            cmd.Parameters.Add(new SqlParameter("@NoPal", palette));
            cmd.Parameters.Add(new SqlParameter("@IdRec", idrec));
            cmd.Parameters.Add(new SqlParameter("@IdCgmt", idcgmt));

            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            int status = reader.GetInt32(0);
            int nbPal = reader.GetInt32(1);

            reader.Close();
            con.Close();

            return new OCLibrePaletteMessageData(MessageResponse.OK, status, nbPal).ToJson();
        }

    }
}
