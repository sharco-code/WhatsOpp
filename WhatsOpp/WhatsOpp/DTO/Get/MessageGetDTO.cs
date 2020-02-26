using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.Model.Local;

namespace WhatsOpp.DTO.Get {
    public class MessageGetDTO {
        public IList<Message> Messages { get; set; }
        public MessageGetDTO(IList<Message> Messages)
        {
            this.Messages = Messages;
        }
    }
}
