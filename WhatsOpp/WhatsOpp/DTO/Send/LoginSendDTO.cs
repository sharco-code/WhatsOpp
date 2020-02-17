using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO.Send {
    public class LoginSendDTO {
        public String Username { get; set; }
        public String Password { get; set; }

        public LoginSendDTO()
        {

        }
        public LoginSendDTO(String username, String password, String email, String phone)
        {
            this.Username = Username;
            this.Password = Password;
        }
    }
}
