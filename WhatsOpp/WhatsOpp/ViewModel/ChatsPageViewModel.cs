using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WhatsOpp.Config;
using WhatsOpp.DAO.Local;
using WhatsOpp.Model.Local;

namespace WhatsOpp.ViewModel {
    public class ChatsPageViewModel {
        public ObservableCollection<Chat> ChatList { get; set; }

        private ChatDAO chatDAO = new ChatDAO(Cfg.Database);
        public ChatsPageViewModel()
        {

            ChatList = chatDAO.GetChats();

        }
    }
}
