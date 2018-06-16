using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AppContacts.Helpers;
using AppContacts.Model;
using Xamarin.Forms;
using AppContacts.View;

namespace AppContacts.ViewModel
{
    public class ContactsPageViewModel
    {
        public ObservableCollection<Grouping<string, Contact>> ContactsLists { get; set; }

        public Command AddContactCommand { get; set; }

        public INavigation Navigation { get; set; }

        public ContactsPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Task.Run(async () => ContactsLists = await App.DataBse.GetAllGrouped()).Wait();
            AddContactCommand = new Command(async () => await GoToDetailContact());
        }

        private async Task GoToDetailContact()
        {
            await Navigation.PushAsync(new ContactDetailPage());
        }
    }
}
