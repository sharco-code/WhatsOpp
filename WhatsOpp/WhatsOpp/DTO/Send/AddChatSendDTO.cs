using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO.Send {
    public class AddChatSendDTO {
        public String TOKEN { get; set; }
        public int ContactID { get; set; }

        public AddChatSendDTO()
        {

        }
        public AddChatSendDTO(String TOKEN, int ContactID)
        {
            this.TOKEN = TOKEN;
            this.ContactID = ContactID;
        }
    }
}
