using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppContacts.Helpers;
using AppContacts.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace AppContacts.Data
{
    public class ContactsManager
    {
        static ContactsManager defaultInstances = new ContactsManager();
        private IMobileServiceClient client;
        private IMobileServiceSyncTable<Contact> contactTable;

        private ContactsManager()
        {
            client = new MobileServiceClient("https://appcontact.azurewebsites.net");
            var store = new MobileServiceSQLiteStore("contact2.db");
            store.DefineTable<Contact>();
            this.client.SyncContext.InitializeAsync(store);
            contactTable = client.GetSyncTable<Contact>();
        }

        public static ContactsManager DefaultInstance
        {
            get
            {
                return defaultInstances;
            }
            set
            {
                defaultInstances = value;
            }
        }


        public async Task<ObservableCollection<Contact>> GetItemsAsync(bool syncItems = false)
        {
            try
            {
                if (syncItems)
                {
                    await SyncAsync();
                }
                IEnumerable<Contact> items =
                                    await contactTable.ToEnumerableAsync();
                return new ObservableCollection<Contact>(items);
            }
            catch (MobileServiceInvalidOperationException mobException)
            {
                Debug.WriteLine($"Exepción: {mobException.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Expeción: {ex.Message}");
            }
            return null;
        }

        public async
            Task<ObservableCollection<Grouping<string, Contact>>>
            GetItemsGroupedAsync(bool syncItems = false)
        {
            try
            {
                if (syncItems)
                {
                    //Si estamos conectados a internet se ejecuta este metodo
                    await SyncAsync();
                }
                IEnumerable<Contact> contacts =
                await GetItemsAsync();
                IEnumerable<Grouping<string, Contact>> sorted =
                    new Grouping<string, Contact>[0];
                if (contacts != null)
                {
                    sorted =
                        from c in contacts
                        orderby c.Nombre
                        group c by c.Nombre[0].ToString()
                        into theGroup
                        select new Grouping<string, Contact>
                        (theGroup.Key, theGroup);
                }
                return new ObservableCollection<Grouping<string, Contact>>(sorted);
            }
            catch (MobileServiceInvalidOperationException mobException)
            {
                Debug.WriteLine($"Expeción: {mobException.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Expeción: {ex.Message}");
            }
            return null;           
        }

        public async Task SyncAsync()
        {
            //Lista que permite almacenar errores
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrores = null;
            try
            {
                await this.client.SyncContext.PushAsync();
                await this.contactTable.PullAsync("idQuery", this.contactTable.CreateQuery());
            }
            catch (MobileServicePushFailedException ex)
            {
                if (ex.PushResult != null)
                {
                    syncErrores = ex.PushResult.Errors;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (syncErrores != null)
            {
                foreach (var item in syncErrores)
                {
                    if (item.OperationKind  == MobileServiceTableOperationKind.Update)
                    {
                        await item.CancelAndUpdateItemAsync(item.Result);
                    }
                    else
                    {
                        await item.CancelAndDiscardItemAsync();
                    }
                }
            }
        }

        public async Task<Contact> GetItemsAsync(string id)
        {
            var items = await contactTable
                             .Where(i => i.Id == id)
                             .ToListAsync();
            return items.FirstOrDefault();
        }

        public async Task SaveItemAsync(Contact item)
        {
            try
            {
                if (item.Id != null)
                {
                    await contactTable.UpdateAsync(item);
                }
                else
                {
                    await contactTable.InsertAsync(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Expeción: {ex.Message}");
            }
        }
    }
}
