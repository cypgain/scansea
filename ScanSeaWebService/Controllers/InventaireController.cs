using Microsoft.AspNetCore.Mvc;
using ScanSeaProtocols;
using ScanSeaProtocols.Core;
using ScanSeaProtocols.Messages;
using ScanSeaWebService.Core;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ScanSeaWebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventaireController : ControllerBase
    {

        [HttpGet("numero/{code}/{agence}/{numero}")]
        public string GetInventaireNumero(string code, int agence, string numero)
        {
            List<Permission> permissions = PermissionsManager.GetPermissionsEnum(code);

            if (!(permissions.Contains(Permission.INVENTAIRE)))
                return new InventaireNumeroMessageData(MessageResponse.ERROR).ToJson();

            SqlConnection con = DatabaseManager.Instance.GetConnectionForCode(code);
            con.Open();

            SqlCommand cmd = new SqlCommand("SCAN_Inventaire_Numero", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IdAgence", agence));
            cmd.Parameters.Add(new SqlParameter("@Numero", numero));

            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            int retIdInventaire = reader.GetInt32(0);
            int retIdRec = reader.GetInt32(1);
            int retIdInv = reader.GetInt32(2);

            reader.Close();
            con.Close();

            return new InventaireNumeroMessageData(MessageResponse.OK, retIdInventaire, retIdRec, retIdInv).ToJson();
        }

        [HttpGet("palette/{code}/{agence}/{inventaire}/{emplacement}/{palette}")]
        public string GetInventairePalette(string code, int agence, int inventaire, string emplacement, string palette)
        {
            List<Permission> permissions = PermissionsManager.GetPermissionsEnum(code);

            if (!(permissions.Contains(Permission.INVENTAIRE)))
                return new InventairePaletteMessageData(MessageResponse.ERROR).ToJson();

            SqlConnection con = DatabaseManager.Instance.GetConnectionForCode(code);
            con.Open();

            SqlCommand cmd = new SqlCommand("SCAN_Inventaire_Palette", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IdAgence", agence));
            cmd.Parameters.Add(new SqlParameter("@IdInventaire", inventaire));
            cmd.Parameters.Add(new SqlParameter("@Emplacement", emplacement));
            cmd.Parameters.Add(new SqlParameter("@NoPal", palette));

            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            int status = reader.GetInt32(0);
            int nbPal = -1;

            if (status != -1)
                nbPal = reader.GetInt32(1);

            reader.Close();
            con.Close();

            return new InventairePaletteMessageData(MessageResponse.OK, status, nbPal).ToJson();
        }

    }
}
