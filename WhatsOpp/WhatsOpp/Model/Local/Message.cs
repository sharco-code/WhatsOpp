using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Model.Local {
    public class Message {
        [PrimaryKey, AutoIncrement]
        public int MessageID { get; set; }
        public int ChatID { get; set; }
        public int Order { get; set; }
        public String Date { get; set; }
        public String Text { get; set; }

        public String Name { get; set; }

    }
}
