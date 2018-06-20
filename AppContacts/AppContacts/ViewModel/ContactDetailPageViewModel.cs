using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppContacts.Data;
using AppContacts.Model;
using Xamarin.Forms;

namespace AppContacts.ViewModel
{
    public class ContactDetailPageViewModel
    {
        public Contact CurrenContacto { get; set; }
        public Command SaveContactCommand { get; set; }
        public Command DeleteContactCommand { get; set; }
        public INavigation Navigation { get; set; }
        public ContactDetailPageViewModel(INavigation navigation, Contact contact=null)
        {
            Navigation = navigation;
            if (contact == null)
            {
                CurrenContacto = new Contact();
            }
            else
            {
                CurrenContacto = contact;
            }
            SaveContactCommand = new Command(async () => await SaveContact());
            //DeleteContactCommand = new Command(async () => await DeleteContact());
        }


        private async Task SaveContact()
        {
            //await App.DataBse.SaveItemAsync(CurrenContacto);
            await ContactsManager.DefaultInstance.SaveItemAsync(CurrenContacto);
            await Navigation.PopToRootAsync();
        }
        //private async Task DeleteContact()
        //{
        //    await App.DataBse.DeleteItemAsync(CurrenContacto);
        //    await Navigation.PopToRootAsync();
        //}
    }
}
