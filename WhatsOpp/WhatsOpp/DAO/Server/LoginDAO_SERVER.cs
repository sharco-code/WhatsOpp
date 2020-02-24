using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WhatsOpp.Config;
using WhatsOpp.DTO;
using WhatsOpp.DTO.Get;
using WhatsOpp.DTO.Send;
using WhatsOpp.Utils;

namespace WhatsOpp.DAO.Server {
    public class LoginDAO {
        private HttpClient Client;

        public LoginDAO()
        {
            Client = new HttpClient();
        }

        public LoginGetDTO Login(LoginSendDTO LoginDTO)
        {
            //return: prefil entero (chats, contactos, perfil)
            string url = Cfg.ConnectionUrl + "/chat/login";

            var uri = new Uri(string.Format(url, string.Empty));
            var LoginDTO_JSON = JsonConvert.SerializeObject(LoginDTO);
            var httpContent = new StringContent(LoginDTO_JSON, Encoding.UTF8, "application/json");
            var response = Client.PostAsync(uri, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                var content = response.Result.Content.ReadAsStringAsync();
                
                LoginGetDTO loginGetDTO = JsonConvert.DeserializeObject<LoginGetDTO>(content.Result);
                if (loginGetDTO.Profile == null)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                    throw new MyException(error.Code, error.Message);
                }
                else
                {
                    return loginGetDTO;
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
