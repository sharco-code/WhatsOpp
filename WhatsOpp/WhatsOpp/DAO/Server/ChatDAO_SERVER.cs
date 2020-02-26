using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WhatsOpp.Config;
using WhatsOpp.DTO.Get;
using WhatsOpp.DTO.Send;
using WhatsOpp.Model.Server;
using WhatsOpp.Utils;

namespace WhatsOpp.DAO.Server {
   public class ChatDAO_SERVER {
        private HttpClient Client;

        public ChatDAO_SERVER()
        {
            Client = new HttpClient();
        }

        public ChatSERVER SendAddChat(AddChatSendDTO addChatSendDTO)
        {
            string url = Cfg.ConnectionUrl + "/chat/chat/add";

            var uri = new Uri(string.Format(url, string.Empty));
            var AddChatSendDTO = JsonConvert.SerializeObject(addChatSendDTO);
            var httpContent = new StringContent(AddChatSendDTO, Encoding.UTF8, "application/json");
            var response = Client.PostAsync(uri, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                var content = response.Result.Content.ReadAsStringAsync();

                ChatSERVER chatSERVER = JsonConvert.DeserializeObject<ChatSERVER>(content.Result);
                if (chatSERVER.Name == null)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                    throw new MyException(error.Code, error.Message);
                }
                else
                {
                    return chatSERVER;
                }
                //TokenGetDTO token = JsonConvert.DeserializeObject<TokenGetDTO>(content.Result);
                /*
                if (token.TOKEN == null)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                    throw new MyException(error.Code, error.Message);
                }
                else
                {
                    return token.TOKEN;
                }
                */
            }
            catch (MyException e)
            {
                var content = response.Result.Content.ReadAsStringAsync();
                Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                throw new MyException(error.Code, error.Message);
            }

        }
        public ChatRefreshGetDTO refresh(TokenSendDTO tokenSendDTO)
        {
            string url = Cfg.ConnectionUrl + "/chat/chats";

            var uri = new Uri(string.Format(url, string.Empty));
            var TokenSendDTO = JsonConvert.SerializeObject(tokenSendDTO);
            var httpContent = new StringContent(TokenSendDTO, Encoding.UTF8, "application/json");
            var response = Client.PostAsync(uri, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                var content = response.Result.Content.ReadAsStringAsync();

                ChatRefreshGetDTO chatSERVER = JsonConvert.DeserializeObject<ChatRefreshGetDTO>(content.Result);
                if (chatSERVER.Chats == null)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                    throw new MyException(error.Code, error.Message);
                }
                else
                {
                    return chatSERVER;
                }
                //TokenGetDTO token = JsonConvert.DeserializeObject<TokenGetDTO>(content.Result);
                /*
                if (token.TOKEN == null)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                    throw new MyException(error.Code, error.Message);
                }
                else
                {
                    return token.TOKEN;
                }
                */
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
