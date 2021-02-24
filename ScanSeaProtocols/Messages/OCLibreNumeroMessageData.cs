namespace ScanSeaProtocols.Messages
{
    public class OCLibreNumeroMessageData : MessageData
    {

        public int RetIdRec { get; set; }
        public int RetIdCgmt { get; set; }

        public OCLibreNumeroMessageData(MessageResponse response) : base(response)
        {
        }

        public OCLibreNumeroMessageData(MessageResponse response, int retIdRec, int retIdCgmt) : this(response)
        {
            RetIdRec = retIdRec;
            RetIdCgmt = retIdCgmt;
        }

        public OCLibreNumeroMessageData()
        {

        }

    }
}
