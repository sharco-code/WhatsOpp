﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WhatsOpp.Config {
    public class Cfg {
        public const string ConnectionUrl = "http://localhost";
        public static string Database = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WhatsOpp.db3");
        public static string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }
}