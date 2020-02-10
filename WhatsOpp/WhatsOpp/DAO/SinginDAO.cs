using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using WhatsOpp.Config;
using WhatsOpp.DTO;
using WhatsOpp.Utils;

namespace WhatsOpp.DAO {
    public class SinginDAO {
        private HttpClient Client;

        public SinginDAO()
        {
            Client = new HttpClient();
        }

        public String Singin(SinginDTO singinDTO) {

            string url = Cfg.ConnectionUrl + "/chat/singin";

            var uri = new Uri(string.Format(url, string.Empty));
            var SinginDTO_JSON = JsonConvert.SerializeObject(singinDTO);
            var httpContent = new StringContent(SinginDTO_JSON, Encoding.UTF8, "application/json");
            var response = Client.PostAsync(uri, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                var content = response.Result.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(content.Result);

                if (token.TOKEN == null)
                {
                    Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                    throw new MyException(error.Code, error.Message);
                } else
                {
                    return token.TOKEN;
                }
                
            }
            catch (MyException e)
            {
                var content = response.Result.Content.ReadAsStringAsync();
                Error error = JsonConvert.DeserializeObject<Error>(content.Result);
                throw new MyException(error.Code, error.Message);
            }
            catch(Newtonsoft.Json.JsonReaderException e)
            {
                Error error = new Error();
                error.Code = 109;
                error.Message = "INVALID_JSON_SYNTAXIS";
                throw new MyException(error.Code, error.Message);
            }
        }

    }
}
