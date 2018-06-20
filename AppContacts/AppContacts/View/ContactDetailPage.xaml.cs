using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppContacts.ViewModel;
using AppContacts.Model;

namespace AppContacts.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactDetailPage : ContentPage
	{
        public ContactDetailPageViewModel ViewModel { get; set; }
        public ContactDetailPage (Contact contact = null)
		{
			InitializeComponent ();
            if (contact == null)
            {
                ViewModel = new ContactDetailPageViewModel(Navigation);
            }
            else
            {
                ViewModel = new ContactDetailPageViewModel(Navigation, contact);
            }
            this.BindingContext = ViewModel;
		}
	}
}