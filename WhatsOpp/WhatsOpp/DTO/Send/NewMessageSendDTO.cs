using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO.Send {
    public class NewMessageSendDTO {
        public String Token { get; set; }
        public int ChatID { get; set; }
        public int Order { get; set; }
        public String Date { get; set; }
        public String Text { get; set; }

        public NewMessageSendDTO(String Token, int ChatID, String date, String Text)
        {
            this.Token = Token;
            this.ChatID = ChatID;
            this.Date = date;
            this.Text = Text;
        }
    }
}
