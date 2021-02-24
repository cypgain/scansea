using Microsoft.AspNetCore.Mvc;
using ScanSeaProtocols;
using ScanSeaProtocols.Messages;
using ScanSeaWebService.Core;

namespace ScanSeaWebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {

        [HttpGet("{code}")]
        public string GetPermissions(string code)
        {
            if (string.IsNullOrEmpty(code))
                return new PermissionsMessageData(MessageResponse.ERROR).ToJson();

            return new PermissionsMessageData(MessageResponse.OK, PermissionsManager.GetPermissions(code)).ToJson();
        }

    }
}
