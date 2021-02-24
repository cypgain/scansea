using Newtonsoft.Json;

namespace ScanSeaProtocols
{
    public class MessageData
    {

        public MessageResponse Response { get; set; }

        public MessageData()
        {

        }

        public MessageData(MessageResponse response)
        {
            Response = response;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); ;
        }

    }
}
