using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.Model;
using WhatsOpp.Model.Local;

namespace WhatsOpp.DAO.Local {
    public class ConfigurationDAO {
        private SQLiteAsyncConnection connection;
        public ConfigurationDAO(string dbpath)
        {
            connection = new SQLiteAsyncConnection(dbpath);
        }

        internal void Insert(Configuration configuration)
        {
            if (configuration == null) throw new Exception("ConfigurationDAO [Insert] - OBJECT_NULL");
            connection.InsertAsync(configuration);
        }
        internal void Save(Configuration configuration)
        {
            if (configuration == null) throw new Exception("ConfigurationDAO [Save] - OBJECT_NULL");
            connection.UpdateAsync(configuration);
            //connection.InsertAsync(configuration);
        }
        internal void Delete(Configuration configuration)
        {
            if (configuration == null) throw new Exception("ConfigurationDAO [Delete] - OBJECT_NULL");
            connection.DeleteAsync(configuration).Wait();
        }
        internal Boolean isEmpty()
        {
            List<Configuration> result = connection.QueryAsync<Configuration>("SELECT * FROM Configuration").Result;
            if ((result == null) || (result.Count == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        internal int GetValue()
        {
            List<Configuration> result = connection.QueryAsync<Configuration>("SELECT * FROM Configuration").Result;
            if ((result == null) || (result.Count == 0))
            {
                throw new Exception("ConfigurationDAO [Delete] - EMPTY_TABLE");
            }
            else
            {
                return result[0].IsFirstTime;
            }
        }
    }
}
