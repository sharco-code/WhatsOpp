using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatsOpp.Config;
using WhatsOpp.DAO.Local;
using WhatsOpp.DAO.Server;
using WhatsOpp.DTO.Send;
using WhatsOpp.Model.Local;
using WhatsOpp.Utils;

namespace WhatsOpp.ViewModel {
    public class ChatViewModel {
        public ObservableCollection<Message> MessageList { get; set; }

        public Chat chat { get; set; }

        private MessageDAO messageLocalDAO = new MessageDAO(Cfg.Database);

        private ProfileDAO profileDAO = new ProfileDAO(Cfg.Database);

        private MessageDAO_SERVER messageDAO_SERVER = new MessageDAO_SERVER();

        public ChatViewModel()
        {
            MessageList = new ObservableCollection<Message>();

        }

            public void getLocalMessages()
        {

            foreach (Message message in messageLocalDAO.GetMessages(chat.ChatID))
            {
                MessageList.Add(message);
            }
        }


        public void sendMessage(String text)
        {
            try
            {

                Message message = messageDAO_SERVER.SendAddMessage(
                    new NewMessageSendDTO(
                        this.profileDAO.GetValue().Token,
                        chat.ChatID,
                        DateTime.Now.ToString("M/d/yyyy"),
                        text));
                MessageList.Add(message);
                this.messageLocalDAO.Insert(message);
            }
            catch (MyException e)
            {
                throw e;
            }
        }
        
        public void refresh()
        {
            if (this.profileDAO.GetValue() == null) return;
            IList<Message> messages = messageDAO_SERVER.refresh(new MessageSendDTO(this.profileDAO.GetValue().Token, this.chat.ChatID)).Messages;
            foreach (Message message in messages)
            {
                messageLocalDAO.Insert(message);
                this.MessageList.Add(message);
            }
            
            //this.MessageList = new ObservableCollection<Message>(this.MessageList.OrderBy(i => i.Order).ToList());


        }
    }
}
