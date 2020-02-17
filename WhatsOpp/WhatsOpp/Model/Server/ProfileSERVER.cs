using SQLite;
using System;

namespace WhatsOpp.Model.Server {
    public class ProfileSERVER {
        public String Username { get; set; }
        public String Name { get; set; }

        public int Phone { get; set; }
        public String Email { get; set; }
        
        public String TOKEN { get; set; }
    }
}
