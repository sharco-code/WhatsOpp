using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.Config;
using WhatsOpp.DAO;
using WhatsOpp.DAO.Local;
using WhatsOpp.DAO.Server;
using WhatsOpp.DTO.Get;
using WhatsOpp.DTO.Send;
using WhatsOpp.Model.Local;
using WhatsOpp.Model.Server;
using WhatsOpp.Utils;

namespace WhatsOpp.ViewModel {
    public class LoginViewModel {
        public LoginSendDTO LoginSendDTO { get; set; }

        private LoginDAO loginDAO = new LoginDAO();

        private ProfileDAO profileDAO = new ProfileDAO(Cfg.Database);
        private ContactDAO contactDAO = new ContactDAO(Cfg.Database);
        private ChatDAO chatDAO = new ChatDAO(Cfg.Database);
        public void SendLogin()
        {
            try
            {
                LoginGetDTO loginGetDTO = loginDAO.Login(LoginSendDTO);

                //Añadir Perfil obtenido del Servidor en la base de datos local
                Profile p = new Profile(loginGetDTO.Profile.Username, loginGetDTO.Profile.Name, loginGetDTO.Profile.Email, loginGetDTO.Profile.Phone, loginGetDTO.Profile.TOKEN);
                profileDAO.Insert(p);

                //Añadir Contactos obtenidos del Servidor en la base de datos local
                foreach (ContactSERVER contactSERVER in loginGetDTO.Contacts)
                {
                    Contact c = new Contact(contactSERVER.ContactID, contactSERVER.Username, contactSERVER.Name);
                    contactDAO.Insert(c);
                }

                //Añadir Chats obtenidos del Servidor en la base de datos local
             
                foreach (ChatSERVER chatSERVER in loginGetDTO.Chats)
                {
                    Chat c = new Chat(chatSERVER.ChatID, chatSERVER.ContactID, chatSERVER.Username, chatSERVER.Name);
                    chatDAO.Insert(c);
                }
                
            }
            catch (MyException e)
            {
                throw e;
            }
        }
    }
}
