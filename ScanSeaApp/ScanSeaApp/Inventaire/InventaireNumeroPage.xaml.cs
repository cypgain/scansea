using ScanSeaApp.Core;
using ScanSeaProtocols;
using ScanSeaProtocols.Messages;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanSeaApp.Inventaire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventaireNumeroPage : ContentPage
    {

        private Utilisateur user;
        private WebServiceConnection connection;

        public InventaireNumeroPage(Utilisateur user)
        {
            InitializeComponent();

            this.user = user;
            connection = new WebServiceConnection();

            Appearing += (object sender, EventArgs e) => NumeroInventaire.Focus();
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
            if (string.IsNullOrEmpty(NumeroInventaire.Text))
            {
                await DisplayAlert("Erreur", "Le numéro d'inventaire est vide", "OK");
                return;
            }

            
            InventaireNumeroMessageData inventaireNumeroMessageData = await connection.GetInventaireNumero(user.Code, user.IdAgence, NumeroInventaire.Text);

            if (inventaireNumeroMessageData.Response == MessageResponse.ERROR)
            {
                await DisplayAlert("Erreur", "Vous n'avez pas la permission", "OK");
                return;
            }

            if (inventaireNumeroMessageData.RetIdInventaire == -1)
            {
                await DisplayAlert("Erreur", "Inventaire inexistant", "OK");
                return;
            }

            if (inventaireNumeroMessageData.RetIdInventaire == -2)
            {
                await DisplayAlert("Erreur", "Inventaire clos", "OK");
                return;
            }

            await Navigation.PushAsync(new InventairePalettePage(user, inventaireNumeroMessageData.RetIdInventaire, inventaireNumeroMessageData.RetIdRec, inventaireNumeroMessageData.RetIdInv));
            Navigation.RemovePage(this);
        }

    }
}