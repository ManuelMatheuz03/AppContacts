using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppContacts.Data;
using AppContacts.Services;
using System.Diagnostics;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AppContacts
{
	public partial class App : Application
	{
        private static ContactDataBase dataBase;

        public static ContactDataBase DataBse
        {
            get
            {
                if (dataBase == null)
                {
                    try
                    {
                        dataBase = new ContactDataBase(DependencyService.Get<IFileHelpers>().GetLocalFilePath("Contacts.db3"));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                return dataBase;
            }
        }
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new View.ContactsPage());
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
	}
}
