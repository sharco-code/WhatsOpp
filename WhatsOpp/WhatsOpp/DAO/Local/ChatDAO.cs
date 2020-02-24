using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WhatsOpp.Model.Local;

namespace WhatsOpp.DAO.Local {
    public class ChatDAO {
        private SQLiteAsyncConnection connection;
        public ChatDAO(string dbpath)
        {
            connection = new SQLiteAsyncConnection(dbpath);
        }
        internal void Insert(Chat chat)
        {
            if (chat == null) throw new Exception("ChatDAO [Insert] - OBJECT_NULL");
            connection.InsertAsync(chat);
        }
        internal void Save(Chat chat)
        {
            if (chat == null) throw new Exception("ChatDAO [Save] - OBJECT_NULL");
            connection.UpdateAsync(chat);
            //connection.InsertAsync(configuration);
        }
        internal void Delete(Chat chat)
        {
            if (chat == null) throw new Exception("ChatDAO [Delete] - OBJECT_NULL");
            connection.DeleteAsync(chat).Wait();
        }
        public ObservableCollection<Chat> GetChats()
        {
            var l = connection.Table<Chat>().ToListAsync().Result;
            return new ObservableCollection<Chat>(l);
        }
        internal int CountRows()
        {
            List<Chat> result = connection.QueryAsync<Chat>("SELECT * FROM Chat").Result;
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
            connection.ExecuteAsync("Delete from Chat");

        }

    }
}
