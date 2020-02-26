using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Model.Local {
    public class Profile {
        [PrimaryKey, AutoIncrement]
        public int ProfileID { get; set; }
        public String Username { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public int Phone { get; set; }
        public String Token { get; set; }
        public Profile()
        {

        }
        public Profile(String Username, String Name, String Email, int Phone, String Token)
        {
            this.Username = Username;
            this.Name = Name;
            this.Email = Email;
            this.Phone = Phone;
            this.Token = Token;
        }
    }
}
