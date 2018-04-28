using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileApps.Models;
using MobileApps.ViewModels;

namespace MobileApps.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        BoletaDetailViewModel viewModel;

        public ItemDetailPage(BoletaDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var boleta = new Boleta
            {
                idBoleta = "Id Boleta 1",
                descripcion = "This is an item description."
            };

            viewModel = new BoletaDetailViewModel(boleta);
            BindingContext = viewModel;
        }
    }
}