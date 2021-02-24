namespace ScanSeaApp.Core
{
    public class Utilisateur
    {

        public string Code { get => code; }
        public int IdSociete { get => idSociete; }
        public int IdAgence { get => idAgence; }

        private string code;
        private int idSociete;
        private int idAgence;

        public Utilisateur(string code, int idSociete, int idAgence)
        {
            this.code = code;
            this.idSociete = idSociete;
            this.idAgence = idAgence;
        }

    }
}
