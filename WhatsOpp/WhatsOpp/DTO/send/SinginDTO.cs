using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO {
    public class SinginDTO {
        public String Username { get; set; }
        public String Password { get; set; }

        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }

        public SinginDTO()
        {

        }
        public SinginDTO(String Username, String Password, String Name, String Email, String Phone)
        {
            this.Username = Username;
            this.Password = Password;
            this.Name = Name;
            this.Email = Email;
            this.Phone = Phone;
        }
    }
}
