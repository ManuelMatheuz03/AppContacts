using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace AppContacts.Model
{
    public class Contact
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("Telefono")]
        public string Telefono { get; set; }
        [JsonProperty("Direccion")]
        public string Direccion { get; set; }
        [JsonProperty("Notas")]
        public string Notas { get; set; }
    }
}
