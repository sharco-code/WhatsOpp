using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WhatsOpp.Config;
using WhatsOpp.DAO.Local;
using WhatsOpp.DAO.Server;
using WhatsOpp.DTO.Send;
using WhatsOpp.Model.Local;
using WhatsOpp.Model.Server;

namespace WhatsOpp.ViewModel {
    public class ChatsPageViewModel {
        public ObservableCollection<Chat> ChatList { get; set; }

        private ChatDAO chatDAO = new ChatDAO(Cfg.Database);
        private ContactDAO contactDAO = new ContactDAO(Cfg.Database);

        private ProfileDAO profileDAO = new ProfileDAO(Cfg.Database);

        private ContactDAO_SERVER contactDAO_SERVER = new ContactDAO_SERVER();
        private ChatDAO_SERVER chatDAO_SERVER = new ChatDAO_SERVER();
        public ChatsPageViewModel()
        {

            ChatList = chatDAO.GetChats();

        }
        public void refresh()
        {
            ChatList.Clear();
            foreach (ChatSERVER chatSERVER in chatDAO_SERVER.refresh(new TokenSendDTO(this.profileDAO.GetValue().TOKEN)).Chats)
            {
                Chat c = new Chat(chatSERVER.ChatID, chatSERVER.ContactID, chatSERVER.Username, chatSERVER.Name);
                chatDAO.Insert(c);
                ChatList.Add(c);
            }

        }
    }
}
