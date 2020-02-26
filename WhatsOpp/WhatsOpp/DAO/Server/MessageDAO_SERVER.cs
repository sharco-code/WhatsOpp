using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WhatsOpp.Config;
using WhatsOpp.DTO.Get;
using WhatsOpp.DTO.Send;
using WhatsOpp.Model.Local;
using WhatsOpp.Utils;

namespace WhatsOpp.DAO.Server {
    public class MessageDAO_SERVER {
        private HttpClient Client;
        public MessageDAO_SERVER()
        {
            Client = new HttpClient();
        }

        public Message SendAddMessage(NewMessageSendDTO newMessageSendDTO)
        {
            string url = Cfg.ConnectionUrl + "/chat/message/add";

            var uri = new Uri(string.Format(url, string.Empty));
            var AddMessageSendDTO = JsonConvert.SerializeObject(newMessageSendDTO);
            var httpContent = new StringContent(AddMessageSendDTO, Encoding.UTF8, "application/json");
            var response = Client.PostAsync(uri, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                var content = response.Result.Content.ReadAsStringAsync();

                Message messageResponse = JsonConvert.DeserializeObject<Message>(content.Result);
                if (messageResponse.Text == null)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                    throw new MyException(error.Code, error.Message);
                }
                else
                {
                    return messageResponse;
                }
            }
            catch (MyException e)
            {
                var content = response.Result.Content.ReadAsStringAsync();
                Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                throw new MyException(error.Code, error.Message);
            }

        }
        public MessageGetDTO refresh(MessageSendDTO messageSendDTO)
        {
            string url = Cfg.ConnectionUrl + "/chat/messages";

            var uri = new Uri(string.Format(url, string.Empty));
            var messageDTO = JsonConvert.SerializeObject(messageSendDTO);
            var httpContent = new StringContent(messageDTO, Encoding.UTF8, "application/json");
            var response = Client.PostAsync(uri, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                var content = response.Result.Content.ReadAsStringAsync();

                MessageGetDTO messages = JsonConvert.DeserializeObject<MessageGetDTO>(content.Result);
                if (messages.Messages == null)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                    throw new MyException(error.Code, error.Message);
                }
                else
                {
                    return messages;
                }
            }
            catch (MyException e)
            {
                var content = response.Result.Content.ReadAsStringAsync();
                Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                throw new MyException(error.Code, error.Message);
            }

        }
    }
}
