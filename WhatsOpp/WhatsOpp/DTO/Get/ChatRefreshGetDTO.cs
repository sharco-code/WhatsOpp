using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.Model.Server;

namespace WhatsOpp.DTO.Get {
    public class ChatRefreshGetDTO {
        public IList<ChatSERVER> Chats { get; set; }
        public ChatRefreshGetDTO(IList<ChatSERVER> Chats)
        {
            this.Chats = Chats;
        }
    }
}
