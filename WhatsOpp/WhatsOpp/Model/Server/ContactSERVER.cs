using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Model.Server {
    public class ContactSERVER {
        public int ContactID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public ContactSERVER()
        {
        }
        public ContactSERVER(int ContactID, string Username, string Name)
        {
            this.ContactID = ContactID;
            this.Username = Username;
            this.Name = Name;
        }
    }
}
