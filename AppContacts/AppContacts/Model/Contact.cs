

namespace AppContacts.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SQLite;

    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Notas { get; set; }
    }
}
