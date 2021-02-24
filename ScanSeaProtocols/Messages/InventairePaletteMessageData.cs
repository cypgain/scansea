namespace ScanSeaProtocols.Messages
{
    public class InventairePaletteMessageData : MessageData
    {

        public int Status { get; set; }
        public int NbPal { get; set; }

        public InventairePaletteMessageData(MessageResponse response) : base(response)
        {
        }

        public InventairePaletteMessageData(MessageResponse response, int status, int nbPal) : this(response)
        {
            Status = status;
            NbPal = nbPal;
        }

        public InventairePaletteMessageData()
        {

        }

    }
}
