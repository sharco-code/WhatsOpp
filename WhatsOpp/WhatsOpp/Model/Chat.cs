﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Model {
    public class Chat {
        [PrimaryKey]
        public int ChatID { get; set; }
        public int ContactID { get; set; }
    }
}
