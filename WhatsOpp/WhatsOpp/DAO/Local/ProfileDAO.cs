using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.Model;

namespace WhatsOpp.DAO.Local {
    public class ProfileDAO {
        private SQLiteAsyncConnection connection;
        public ProfileDAO(string dbpath)
        {
            connection = new SQLiteAsyncConnection(dbpath);
        }
        internal void Insert(Profile profile)
        {
            if (profile == null) throw new Exception("ProfileDAO [Insert] - OBJECT_NULL");
            connection.InsertAsync(profile);
        }
        internal void Save(Profile profile)
        {
            if (profile == null) throw new Exception("ProfileDAO [Save] - OBJECT_NULL");
            connection.UpdateAsync(profile);
            //connection.InsertAsync(configuration);
        }
        internal void Delete(Profile profile)
        {
            if (profile == null) throw new Exception("ProfileDAO [Delete] - OBJECT_NULL");
            connection.DeleteAsync(profile).Wait();
        }
        internal Profile GetValue()
        {
            List<Profile> result = connection.QueryAsync<Profile>("SELECT * FROM Profile").Result;
            if ((result == null) || (result.Count == 0))
            {
                throw new Exception("ProfileDAO [Delete] - EMPTY_TABLE");
            }
            else
            {
                return result[0];
            }
        }
    }
}
