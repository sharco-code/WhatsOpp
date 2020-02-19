using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOpp.Model.Local {
    public class Configuration {
        [PrimaryKey]
        public int IsFirstTime { get; set; }

        public Configuration()
        {

        }
        public Configuration(int IsFirstTime)
        {
            this.IsFirstTime = IsFirstTime;
        }
    }
}
