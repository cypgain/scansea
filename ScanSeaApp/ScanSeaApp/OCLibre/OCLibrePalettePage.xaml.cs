using ScanSeaApp.Core;
using ScanSeaProtocols.Messages;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace ScanSeaApp.OCLibre
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OCLibrePalettePage : ContentPage
    {

        private WebServiceConnection connection;
        private Utilisateur user;
        private int retIdRec;
        private int retIdCgmt;

        public OCLibrePalettePage(Utilisateur user, int retIdRec, int retIdCgmt)
        {
            InitializeComponent();

            connection = new WebServiceConnection();

            this.user = user;
            this.retIdRec = retIdRec;
            this.retIdCgmt = retIdCgmt;
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            Next();
        }

        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OCLibreNumeroPage(user));
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
                    PaletteCodeEntry.Text = result.Text;
                    Next();
                });
            };
        }

        private async void Next()
        {
            if (string.IsNullOrEmpty(PaletteCodeEntry.Text))
            {
                await DisplayAlert("Erreur", "Le code palette est vide", "OK");
                return;
            }

            OCLibrePaletteMessageData ocLibrePaletteMessageData = await connection.GetOcLibrePalette(user.Code, user.IdAgence, PaletteCodeEntry.Text, retIdRec, retIdCgmt);

            if (ocLibrePaletteMessageData.Status == -1)
            {
                await DisplayAlert("Erreur", "Palette inconnue", "OK");
                return;
            }

            if (ocLibrePaletteMessageData.Status == -2)
            {
                await DisplayAlert("Erreur", "Palette déjà associé à cet OC", "OK");
                return;
            }

            if (ocLibrePaletteMessageData.Status == -3)
            {
                await DisplayAlert("Erreur", "Cet OC n'est pas libre", "OK");
                return;
            }

            if (ocLibrePaletteMessageData.Status == -4)
            {
                await DisplayAlert("Erreur", "Chargement inexistant", "OK");
                return;
            }

            if (ocLibrePaletteMessageData.Status == -5)
            {
                await DisplayAlert("Erreur", "Palette sous douane", "OK");
                return;
            }

            await DisplayAlert("OK", "Palette scannée", "OK");
            PaletteCodeEntry.Text = "";
        }

    }
}