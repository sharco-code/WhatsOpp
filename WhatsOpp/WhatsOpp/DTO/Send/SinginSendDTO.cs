using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO.Send {
    public class SinginSendDTO {
        public String Username { get; set; }
        public String Password { get; set; }

        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }

        public SinginSendDTO()
        {

        }
        public SinginSendDTO(String Username, String Password, String Name, String Email, String Phone)
        {
            this.Username = Username;
            this.Password = Password;
            this.Name = Name;
            this.Email = Email;
            this.Phone = Phone;
        }
    }
}
