using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.Config;
using WhatsOpp.DAO.Local;
using WhatsOpp.DTO.Get;
using WhatsOpp.Model.Local;

namespace WhatsOpp.ViewModel {
    public class ProfilePageViewModel {

        public Profile Profile { get; set; }

        private ProfileDAO profileDAO = new ProfileDAO(Cfg.Database);
        private ContactDAO contactDAO = new ContactDAO(Cfg.Database);
        private ChatDAO chatDAO = new ChatDAO(Cfg.Database);
        public ProfilePageViewModel()
        {

            Profile = profileDAO.GetValue();
        }

        public void LogOut()
        {
            profileDAO.DeleteAll();
            contactDAO.DeleteAll();
            chatDAO.DeleteAll();
        }
    }
}
