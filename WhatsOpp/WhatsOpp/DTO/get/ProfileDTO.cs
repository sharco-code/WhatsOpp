using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO.get {
    public class ProfileDTO {
        public String Token { get; set; }
        public String Username { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }

        public ProfileDTO()
        {

        }
        public ProfileDTO(String Username, String TOKEN, String Name, String Email, String Phone)
        {
            this.Username = Username;
            this.Token = TOKEN;
            this.Name = Name;
            this.Email = Email;
            this.Phone = Phone;
        }
    }
}
