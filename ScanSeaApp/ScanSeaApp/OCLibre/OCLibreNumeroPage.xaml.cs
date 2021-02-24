using ScanSeaApp.Core;
using ScanSeaProtocols;
using ScanSeaProtocols.Messages;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanSeaApp.OCLibre
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OCLibreNumeroPage : ContentPage
    {

        private WebServiceConnection connection;
        private Utilisateur user;

        public OCLibreNumeroPage(Utilisateur user)
        {
            InitializeComponent();

            connection = new WebServiceConnection();
            this.user = user;

            Appearing += (object sender, EventArgs e) => NumeroOcLibre.Focus();
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            Next();
        }

        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage(user));
            Navigation.RemovePage(this);
        }

        void OnNextButtonClicked(object sender, EventArgs e)
        {
            Next();
        }

        private async void Next()
        {
            if (string.IsNullOrEmpty(NumeroOcLibre.Text))
            {
                await DisplayAlert("Erreur", "Le numéro OC Libre est vide", "OK");
                return;
            }

            OCLibreNumeroMessageData ocLibreNumeroMessageData = await connection.GetOcLibreNumero(user.Code, user.IdAgence, NumeroOcLibre.Text);

            if (ocLibreNumeroMessageData.Response == MessageResponse.ERROR)
            {
                await DisplayAlert("Erreur", "Vous n'avez pas la permission", "OK");
                return;
            }

            if (ocLibreNumeroMessageData.RetIdRec == -1)
            {
                await DisplayAlert("Erreur", "Chargement inexistant", "OK");
                return;
            }

            if (ocLibreNumeroMessageData.RetIdRec == -2)
            {
                await DisplayAlert("Erreur", "Cet OC n'est pas libre", "OK");
                return;
            }


            await Navigation.PushAsync(new OCLibrePalettePage(user, ocLibreNumeroMessageData.RetIdRec, ocLibreNumeroMessageData.RetIdCgmt));
            Navigation.RemovePage(this);
        }

    }
}