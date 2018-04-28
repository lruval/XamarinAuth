using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileApps.Models;

namespace MobileApps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Boleta Boleta { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Boleta = new Boleta
            {
                idBoleta = "Id Boleta",
                descripcion = "Descripcion de la boleta"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Boleta);
            await Navigation.PopModalAsync();
        }
    }
}