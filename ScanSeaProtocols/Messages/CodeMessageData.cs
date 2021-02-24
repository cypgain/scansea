namespace ScanSeaProtocols.Messages
{
    public class CodeMessageData : MessageData
    {

        public int IdSociete { get; set; }
        public int IdAgence { get; set; }

        public CodeMessageData(MessageResponse response) : base(response)
        {
        }

        public CodeMessageData(MessageResponse response, int idSociete, int idAgence) : this(response)
        {
            IdSociete = idSociete;
            IdAgence = idAgence;
        }

        public CodeMessageData()
        {

        }

    }
}
