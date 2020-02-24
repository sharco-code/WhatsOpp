using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.Model.Server;

namespace WhatsOpp.DTO.Get {
    public class ContactsRefreshGetDTO {

        public IList<ContactSERVER> Contacts { get; set; }
        public ContactsRefreshGetDTO(IList<ContactSERVER> Contacts)
        {
            this.Contacts = Contacts;
        }
    }
}
