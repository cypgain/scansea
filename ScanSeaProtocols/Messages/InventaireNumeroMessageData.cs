namespace ScanSeaProtocols.Messages
{
    public class InventaireNumeroMessageData : MessageData
    {

        public int RetIdInventaire { get; set; }
        public int RetIdRec { get; set; }
        public int RetIdInv { get; set; }

        public InventaireNumeroMessageData(MessageResponse response) : base(response)
        {
        }

        public InventaireNumeroMessageData(MessageResponse response, int retIdInventaire, int retIdRec, int retIdInv) : this(response)
        {
            RetIdInventaire = retIdInventaire;
            RetIdRec = retIdRec;
            RetIdInv = retIdInv;
        }

        public InventaireNumeroMessageData()
        {

        }

    }
}
