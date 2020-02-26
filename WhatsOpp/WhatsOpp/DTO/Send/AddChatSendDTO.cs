using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO.Send {
    public class AddChatSendDTO {
        public String Token { get; set; }
        public int ContactID { get; set; }

        public AddChatSendDTO()
        {

        }
        public AddChatSendDTO(String Token, int ContactID)
        {
            this.Token = Token;
            this.ContactID = ContactID;
        }
    }
}
