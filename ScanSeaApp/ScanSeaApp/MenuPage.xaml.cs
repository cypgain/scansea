using ScanSeaApp.Core;
using ScanSeaApp.Inventaire;
using ScanSeaApp.OCLibre;
using ScanSeaProtocols.Core;
using ScanSeaProtocols.Messages;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanSeaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        private WebServiceConnection connection;
        private Utilisateur user;

        public MenuPage(Utilisateur user)
        {
            InitializeComponent();

            connection = new WebServiceConnection();
            this.user = user;
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            PermissionsMessageData permissionsMessageData = await connection.GetPermissions(user.Code);

            var grid = new Grid()
            {
                Margin = new Thickness(20, 35, 20, 20)
            };

            int row = 0;

            // Bouton Oc Libre
            if (permissionsMessageData.Permissions.Contains((int)Permission.OC_LIBRE))
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                var btn = new Button()
                {
                    Text = "OC Libre"
                };

                btn.Clicked += OnOCLibreButtonClick;

                grid.Children.Add(btn, 0, row);
                row++;
            }

            // Bouton Prepa Commande
            if (permissionsMessageData.Permissions.Contains((int)Permission.PREPA_COMMANDE))
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                var btn = new Button()
                {
                    Text = "Prepa Commande"
                };

                btn.Clicked += OnPrepaCommandeButtonClick;

                grid.Children.Add(btn, 0, row);
                row++;
            }

            // Bouton Inventaire
            if (permissionsMessageData.Permissions.Contains((int)Permission.INVENTAIRE))
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                var btn = new Button()
                {
                    Text = "Inventaire"
                };

                btn.Clicked += OnInventaireButtonClick;

                grid.Children.Add(btn, 0, row);
                row++;
            }

            // Bouton Quitter
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            var leaveButton = new Button()
            {
                Text = "Quitter"
            };

            leaveButton.Clicked += OnQuitButtonClick;

            grid.Children.Add(leaveButton, 0, row);

            Content = grid;
        }

        async void OnOCLibreButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OCLibreNumeroPage(user));
            Navigation.RemovePage(this);
        }

        async void OnPrepaCommandeButtonClick(object sender, EventArgs e)
        {

        }

        async void OnInventaireButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventaireNumeroPage(user));
            Navigation.RemovePage(this);
        }

        async void OnQuitButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
            Navigation.RemovePage(this);
        }
    }
}