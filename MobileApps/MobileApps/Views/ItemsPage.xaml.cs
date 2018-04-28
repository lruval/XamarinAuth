using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileApps.Models;
using MobileApps.Views;
using MobileApps.ViewModels;

namespace MobileApps.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        BoletasViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new BoletasViewModel();
        }

        // Auth

        bool authenticated = false;

        async void Login_Clicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
                authenticated = await App.Authenticator.Authenticate();

            // Set syncItems to true to synchronize the data on startup when offline is enabled.
            if (authenticated == true)
            {
                //await RefreshItems(true, syncItems: false);
            }


        }
        // End Auth

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var boleta = args.SelectedItem as Boleta;
            if (boleta == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new BoletaDetailViewModel(boleta)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Boletas.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}