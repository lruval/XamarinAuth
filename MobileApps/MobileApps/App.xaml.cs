using System;

using MobileApps.Views;
using MobileApps.Services;
using Xamarin.Forms;

namespace MobileApps
{
	public partial class App : Application
	{
		public static bool UseMockDataStore = false;
		public static string AzureMobileAppUrl = "https://hackfestcostarica.azurewebsites.net";

		public App ()
		{
			InitializeComponent();

			if (UseMockDataStore)
				DependencyService.Register<MockDataStore>();
			else
				DependencyService.Register<AzureDataStore>();

            MainPage = new MainPage();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }
    }
}
