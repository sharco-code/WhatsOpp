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
using WhatsOpp.Utils;

namespace WhatsOpp.ViewModel {
    public class ContactPageViewModel {
        public AddContactSendDTO AddContactSendDTO { get; set; }
        public ObservableCollection<Contact> ContactList { get; set; }

        private ContactDAO contactDAO = new ContactDAO(Cfg.Database);

        private ProfileDAO profileDAO = new ProfileDAO(Cfg.Database);

        private ContactDAO_SERVER contactDAO_SERVER = new ContactDAO_SERVER();
        private ChatDAO_SERVER chatDAO_SERVER = new ChatDAO_SERVER();
        public ContactPageViewModel()
        {

            ContactList = contactDAO.GetContacts();

        }
        public void sendAddContact()
        {
            try
            {

                this.AddContactSendDTO.Token = this.profileDAO.GetValue().TOKEN;
                ContactSERVER c = contactDAO_SERVER.SendAddContact(AddContactSendDTO);

                /*
                 * contactDAO.Insert(new Contact(c.ContactID,c.Username,c.Name));
                ContactList.Add(new Contact(c.ContactID, c.Username, c.Name));
                 */

                refresh();


            }
            catch (MyException e)
            {
                throw e;
            }
        }
        public void sendAddChat(Contact contact)
        {
            try
            {

                chatDAO_SERVER.SendAddChat(new AddChatSendDTO(this.profileDAO.GetValue().TOKEN, contact.ContactID));
            }
            catch (MyException e)
            {
                throw e;
            }
        }
        public void refresh()
        {
            ContactList.Clear();
            foreach (ContactSERVER contactSERVER in contactDAO_SERVER.refresh(new TokenSendDTO(this.profileDAO.GetValue().TOKEN)).Contacts)
            {
                Contact c = new Contact(contactSERVER.ContactID, contactSERVER.Username, contactSERVER.Name);
                contactDAO.Insert(c);
                ContactList.Add(c);
            }

        }
    }
}

