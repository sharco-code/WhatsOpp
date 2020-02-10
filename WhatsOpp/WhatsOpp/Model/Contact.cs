using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Model {
    public class Contact {
        public int ContactID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public Contact(int ContactID, string Username, string Name)
        {
            this.ContactID = ContactID;
            this.Username = Username;
            this.Name = Name;
        }
    }
}
