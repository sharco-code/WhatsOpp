using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WhatsOpp.Config;
using WhatsOpp.DAO.Local;
using WhatsOpp.Model.Local;

namespace WhatsOpp.ViewModel {
    public class ContactPageViewModel {
        public ObservableCollection<Contact> ContactList { get; set; }

        private ContactDAO contactDAO = new ContactDAO(Cfg.Database);
        public ContactPageViewModel()
        {

            ContactList = contactDAO.GetContacts();

        }
    }
}

