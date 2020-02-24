using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.DTO.Send {
    public class TokenSendDTO {
        public String Token { get; set; }
        public TokenSendDTO()
        {

        }
        public TokenSendDTO(String TOKEN)
        {
            this.Token = TOKEN;

        }
    }
}
