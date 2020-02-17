using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.DAO;
using WhatsOpp.DAO.Server;
using WhatsOpp.DTO.Send;
using WhatsOpp.Utils;

namespace WhatsOpp.ViewModel {
    public class SinginViewModel {

        public SinginSendDTO SinginSendDTO { get; set; }

        private SinginDAO singinDAO = new SinginDAO();

        public void SendSingin()
        {
            try
            {
                string token = singinDAO.Singin(SinginSendDTO);
            }
            catch(MyException e)
            {
                throw e;
            }
        }
    }
}
