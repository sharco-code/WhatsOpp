using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO.Send {
    public class AddContactSendDTO {

        public String Token { get; set; }
        public String Username { get; set; }

        public AddContactSendDTO()
        {

        }
        public AddContactSendDTO(String Token, String Username)
        {
            this.Token = Token;
            this.Username = Username;
        }
    }
}
