using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO {
    public class LoginDTO {
        public String Username { get; set; }
        public String Password { get; set; }

        public LoginDTO()
        {

        }
        public LoginDTO(String username, String password, String email, String phone)
        {
            this.Username = Username;
            this.Password = Password;
        }
    }
}
