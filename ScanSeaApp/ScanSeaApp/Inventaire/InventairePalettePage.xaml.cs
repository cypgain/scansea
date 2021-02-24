using ScanSeaApp.Core;
using ScanSeaProtocols.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace ScanSeaApp.Inventaire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventairePalettePage : ContentPage
    {

        private WebServiceConnection connection;
        private Utilisateur user;
        private int retIdInventaire;
        private int retIdRec;
        private int retIdInv;

        public InventairePalettePage(Utilisateur user, int retIdInventaire, int retIdRec, int retIdInv)
        {
            InitializeComponent();

            connection = new WebServiceConnection();
            this.user = user;
            this.retIdInventaire = retIdInventaire;
            this.retIdRec = retIdRec;
            this.retIdInv = retIdInv;

            Appearing += (object sender, EventArgs e) => EmplacementEntry.Focus();
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            Next();
        }

        void OnEmplacementTextChanged(object sender, EventArgs e)
        {
            SetPaletteAmountLabel(0);
            NbPaletteLabel.IsVisible = false;
        }

        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventaireNumeroPage(user));
            Navigation.RemovePage(this);
        }

        void OnNextButtonClicked(object sender, EventArgs e)
        {
            Next();
        }

        async void OnScanButtonClicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    PaletteEntry.Text = result.Text;
                    Next();
                });
            };
        }

        private async void Next()
        {
            if (string.IsNullOrEmpty(PaletteEntry.Text))
            {
                await DisplayAlert("Erreur", "Le code palette est vide", "OK");
                return;
            }

            if (string.IsNullOrEmpty(EmplacementEntry.Text))
            {
                await DisplayAlert("Erreur", "L'emplacement est vide", "OK");
                return;
            }

            
            InventairePaletteMessageData inventairePaletteMessageData = await connection.GetInventairePalette(user.Code, user.IdAgence, retIdInventaire, EmplacementEntry.Text, PaletteEntry.Text);

            if (inventairePaletteMessageData.Status == -1)
            {
                await DisplayAlert("Erreur", "Inventaire inexistant", "OK");
                return;
            }

            if (inventairePaletteMessageData.Status == -2)
            {
                await DisplayAlert("Erreur", "Inventaire clos", "OK");
                return;
            }

            if (inventairePaletteMessageData.Status == -3)
            {
                await DisplayAlert("Erreur", "Déjà scanné à cet emplacement", "OK");
                return;
            }

            if (inventairePaletteMessageData.Status == -4)
            {
                await DisplayAlert("Erreur", "Une erreur est survenue", "OK");
                return;
            }

            await DisplayAlert("OK", "Palette scannée", "OK");
            PaletteEntry.Text = "";
            SetPaletteAmountLabel(inventairePaletteMessageData.NbPal);
        }

        private void SetPaletteAmountLabel(int amount)
        {
            NbPaletteLabel.IsVisible = true;
            NbPaletteLabel.Text = $"Nombre de palette pour l'emplacement : {amount}";
        }

    }
}