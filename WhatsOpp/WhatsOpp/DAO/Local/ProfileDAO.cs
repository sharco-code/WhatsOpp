using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using WhatsOpp.Model;
using WhatsOpp.Model.Local;

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
        internal Boolean isEmpty()
        {
            List<Profile> result = connection.QueryAsync<Profile>("SELECT * FROM Profile").Result;
            if ((result == null) || (result.Count == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal Profile GetValue()
        {
            List<Profile> result = connection.QueryAsync<Profile>("SELECT * FROM Profile").Result;
            if ((result == null) || (result.Count == 0))
            {
                return null;
            }
            else
            {
                return result[0];
            }
        }
        internal int CountRows()
        {
            List<Profile> result = connection.QueryAsync<Profile>("SELECT * FROM Profile").Result;
            if ((result == null) || (result.Count == 0))
            {
                return 0;
            }
            else
            {
                return result.Count;
            }
        }
        internal void DeleteAll()
        {
            connection.ExecuteAsync("Delete from Profile");

        }
    }
}
