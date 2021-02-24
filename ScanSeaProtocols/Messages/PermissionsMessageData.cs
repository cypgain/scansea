using System.Collections.Generic;

namespace ScanSeaProtocols.Messages
{
    public class PermissionsMessageData : MessageData
    {

        public List<int> Permissions { get; set; }

        public PermissionsMessageData(MessageResponse response) : base(response)
        {
        }

        public PermissionsMessageData(MessageResponse response, List<int> permissions) : this(response)
        {
            Permissions = permissions;
        }

        public PermissionsMessageData()
        {

        }

    }
}
