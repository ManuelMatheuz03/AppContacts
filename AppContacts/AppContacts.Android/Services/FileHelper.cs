using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using System.IO;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppContacts.Services;
using Xamarin.Forms;
using AppContacts.Droid.Services;

[assembly: Dependency(typeof(FileHelper))]
namespace AppContacts.Droid.Services
{
    public class FileHelper : IFileHelpers
    {
        public string GetLocalFilePath(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}