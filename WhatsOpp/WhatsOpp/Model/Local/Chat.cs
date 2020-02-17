using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Model.Local {
    public class Chat {
        [PrimaryKey]
        public int ChatID { get; set; }
        public int ContactID { get; set; }

        public String Username { get; set; }
        public String Name { get; set; }
        public Chat() { }
        public Chat(int ChatID, int ContactID, String Username, String Name)
        {
            this.ChatID = ChatID;
            this.ContactID = ContactID;
            this.Username = Username;
            this.Name = Name;
        }
    }
}
