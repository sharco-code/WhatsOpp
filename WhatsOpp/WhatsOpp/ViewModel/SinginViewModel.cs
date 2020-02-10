using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.DAO;
using WhatsOpp.DTO;
using WhatsOpp.Utils;

namespace WhatsOpp.ViewModel {
    public class SinginViewModel {

        public SinginDTO SinginDTO { get; set; }

        private SinginDAO singinDAO = new SinginDAO();

        public void SendSingin()
        {
            try
            {
                string token = singinDAO.Singin(SinginDTO);
            }
            catch(MyException e)
            {
                throw e;
            }
        }
    }
}
