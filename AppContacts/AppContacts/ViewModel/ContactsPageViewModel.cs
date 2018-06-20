using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AppContacts.Helpers;
using AppContacts.Model;
using Xamarin.Forms;
using AppContacts.View;
using AppContacts.Data;
using Plugin.Connectivity;

namespace AppContacts.ViewModel
{
    public class ContactsPageViewModel
    {
        public ObservableCollection<Grouping<string, Contact>> ContactsLists { get; set; }

        public Contact CurrentContact { get; set; }
        public Command AddContactCommand { get; set; }

        public INavigation Navigation { get; set; }
        public Command ItemTappedCommand { get; }

        public ContactsPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            //Indica si esta conectado
            var isConnected = CrossConnectivity.Current.IsConnected;
            Task.Run(async () => ContactsLists = await ContactsManager.DefaultInstance.GetItemsGroupedAsync(isConnected)).Wait();
            AddContactCommand = new Command(async () => await GoToContactDetailPage());
            ItemTappedCommand = new Command(async () => GoToContactDetailPage(CurrentContact));
        }

        public async Task GoToContactDetailPage(Contact contact = null)
        {
            if (contact == null)
            {
                await Navigation.PushAsync(new ContactDetailPage());
            }
            else
            {
                await Navigation.PushAsync(new ContactDetailPage(CurrentContact));
            }
        }

        private async Task GoToDetailContact()
        {
            await Navigation.PushAsync(new ContactDetailPage());
        }
    }
}
