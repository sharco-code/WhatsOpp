using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Model {
    public class Profile {
        [PrimaryKey, AutoIncrement]
        public int ProfileID { get; set; }
        public String Username { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public int PhoneNumber { get; set; }
        public String TOKEN { get; set; }
    }
}
