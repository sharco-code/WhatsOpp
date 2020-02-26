using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO.Send {
    public class MessageSendDTO {
        public String Token { get; set; }
        public int ChatID { get; set; }

        public MessageSendDTO(String Token, int ChatID)
        {
            this.Token = Token;
            this.ChatID = ChatID;
        }
    }
}
