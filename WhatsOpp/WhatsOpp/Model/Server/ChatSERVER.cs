using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Model.Server {
    public class ChatSERVER {
        public int ChatID { get; set; }
        public int ContactID { get; set; }

        public String Username { get; set; }
        public String Name { get; set; }
    }
}
