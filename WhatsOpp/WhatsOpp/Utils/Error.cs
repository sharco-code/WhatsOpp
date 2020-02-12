using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Utils {
    public class Error {

        public const int SINGIN_ERROR = 101;
        public const int LOGIN_ERROR = 102;
        public const int GET_MESSAGE_ERROR = 103;
        public const int SEND_MESSAGE_ERROR = 104;
        public const int CONTACT_ADD_ERROR = 105;
        public const int CHAT_ADD_ERROR = 106;
        public const int TOKEN_VERIFY_ERROR = 107;
        public const int CONNECTION_LOST = 108;
        public const int INVALID_JSON_SYNTAXIS = 109;

        public const int PASSWORD_OR_USER_INCORRECT = 201;
        public const int USER_ALREADY_EXIST = 202;
        public const int TOKEN_INVALID = 203;
        public const int NO_USERNAME = 204;
        public const int NO_PASSWORD = 205;
        public const int NO_EMAIL = 206;
        public const int NO_PHONE = 207;
        public const int NO_NAME = 208;
        public const int USERNAME_INVALID = 209;

        public int Code { get; set; }
        public string Message { get; set; }
    }
}
