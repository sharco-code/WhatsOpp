using System;
namespace WhatsOpp.Utils {
    public class MyException : Exception {
        public int Code { get; set; }
        public string Message { get; set; }

        public MyException(int Code, string Message)
        {
            this.Code = Code;
            this.Message = Message;
        }
    }
}
