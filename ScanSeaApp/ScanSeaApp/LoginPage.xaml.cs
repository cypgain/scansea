using ScanSeaApp.Core;
using ScanSeaProtocols;
using ScanSeaProtocols.Messages;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace ScanSeaApp
{
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {

        private WebServiceConnection connection;

        public LoginPage()
        {
            InitializeComponent();

            connection = new WebServiceConnection();

            Appearing += (object sender, EventArgs e) => CodeEntry.Focus();
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            Login();
        }

        void OnLoginButtonClicked(object sender, EventArgs e)
        {
            Login();
        }

        private async void Login()
        {
            string code = CodeEntry.Text;

            if (string.IsNullOrEmpty(code))
            {
                await DisplayAlert("Erreur", "Le code ne doit pas être vide !", "OK");
                return;
            }

            CodeMessageData codeMessageData = await connection.Login(code);

            if (codeMessageData == null)
            {
                await DisplayAlert("Erreur", "Connexion au service web impossible !", "OK");
                return;
            }

            if (codeMessageData.Response != MessageResponse.OK)
            {
                await DisplayAlert("Erreur", "Le code est n'est pas correct !", "OK");
                return;
            }

            await Navigation.PushAsync(new MenuPage(new Utilisateur(code, codeMessageData.IdSociete, codeMessageData.IdAgence)));
            Navigation.RemovePage(this);
        }

    }
}
