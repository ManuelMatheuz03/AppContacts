using System;
using System.Collections.Generic;
using System.Text;

namespace AppContacts.Services
{
    public interface IFileHelpers
    {
        string GetLocalFilePath(string fileName);
    }
}
