using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Model {
    public class Message {
        [PrimaryKey, AutoIncrement]
        private int MessageID { get; set; }
        private int ChatID { get; set; }
        private int Order { get; set; }
        private String Date { get; set; }
        private String Text { get; set; }
        
    }
}
