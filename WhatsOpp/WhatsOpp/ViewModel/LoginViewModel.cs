using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.DAO;
using WhatsOpp.DTO;
using WhatsOpp.Utils;

namespace WhatsOpp.ViewModel {
    public class LoginViewModel {
        public LoginDTO LoginDTO { get; set; }

        private LoginDAO loginDAO = new LoginDAO();

        public void SendLogin()
        {
            try
            {
                string token = loginDAO.Login(LoginDTO);
            }
            catch (MyException e)
            {
                throw e;
            }
        }
    }
}
