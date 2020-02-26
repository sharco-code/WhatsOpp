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
    public class ContactDAO_SERVER {
        private HttpClient Client;

        public ContactDAO_SERVER()
        {
            Client = new HttpClient();
        }
        public ContactSERVER SendAddContact(AddContactSendDTO addContactSendDTO)
        {

            string url = Cfg.ConnectionUrl + "/chat/contact/add";

            var uri = new Uri(string.Format(url, string.Empty));
            var AddContactSendDTO = JsonConvert.SerializeObject(addContactSendDTO);
            var httpContent = new StringContent(AddContactSendDTO, Encoding.UTF8, "application/json");
            var response = Client.PostAsync(uri, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                var content = response.Result.Content.ReadAsStringAsync();

                ContactSERVER contactSERVER = JsonConvert.DeserializeObject<ContactSERVER>(content.Result);
                if (contactSERVER.Name == null)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                    throw new MyException(error.Code, error.Message);
                }
                else
                {
                    return contactSERVER;
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
            } catch (Exception x)
            {
                var content = response.Result.Content.ReadAsStringAsync();
                Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                throw new MyException(error.Code, error.Message);
            }

        }

        public ContactsRefreshGetDTO refresh(TokenSendDTO tokenSendDTO)
        {
            string url = Cfg.ConnectionUrl + "/chat/contact";

            var uri = new Uri(string.Format(url, string.Empty));
            var TokenSendDTO = JsonConvert.SerializeObject(tokenSendDTO);
            var httpContent = new StringContent(TokenSendDTO, Encoding.UTF8, "application/json");
            var response = Client.PostAsync(uri, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                var content = response.Result.Content.ReadAsStringAsync();

                ContactsRefreshGetDTO contactSERVER = JsonConvert.DeserializeObject<ContactsRefreshGetDTO>(content.Result);
                if (contactSERVER.Contacts == null)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                    throw new MyException(error.Code, error.Message);
                }
                else
                {
                    return contactSERVER;
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
