namespace ScanSeaProtocols.Messages
{
    public class OCLibrePaletteMessageData : MessageData
    {

        public int Status { get; set; }
        public int NbPal { get; set; }

        public OCLibrePaletteMessageData(MessageResponse response) : base(response)
        {
        }

        public OCLibrePaletteMessageData(MessageResponse response, int status, int nbPal) : this(response)
        {
            Status = status;
            NbPal = nbPal;
        }

        public OCLibrePaletteMessageData()
        {

        }

    }
}
