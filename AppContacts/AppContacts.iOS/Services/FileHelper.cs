using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using AppContacts.iOS.Services;
using AppContacts.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace AppContacts.iOS.Services
{
    public class FileHelper : IFileHelpers
    {
        public string GetLocalFilePath(string fileName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "DataBases");
            if (!Directory.Exists(libFolder))
            {
            }
            return Path.Combine(libFolder, fileName);

        }
    }
}