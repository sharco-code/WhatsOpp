using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.Model;
using WhatsOpp.Model.Server;

namespace WhatsOpp.DTO.Get {
    public class LoginGetDTO {

        public IList<ChatSERVER> Chats { get; set; }

        public IList<ContactSERVER> Contacts { get; set; }
        public ProfileSERVER Profile { get; set; }
        public LoginGetDTO()
        {

        }
        public LoginGetDTO(ProfileSERVER Profile, IList<ChatSERVER> Chats, IList<ContactSERVER> Contacts)
        {
            this.Profile = Profile;
            this.Chats = Chats;
            this.Contacts = Contacts;
        }
    }
}
