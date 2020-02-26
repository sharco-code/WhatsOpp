using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WhatsOpp.Model.Local;

namespace WhatsOpp.DAO.Local {
    public class MessageDAO {
        private SQLiteAsyncConnection connection;
        public MessageDAO(string dbpath)
        {
            connection = new SQLiteAsyncConnection(dbpath);
        }

        internal void Insert(Message message)
        {
            if (message == null) throw new Exception("MessageDAO [Insert] - OBJECT_NULL");
            connection.InsertAsync(message);
        }


        public ObservableCollection<Message> GetMessages(int messageid)
        {
            List<Message> result = connection.QueryAsync<Message>("SELECT * FROM Message WHERE chatid = " + messageid).Result;
            return new ObservableCollection<Message>(result);
        }

        internal void DeleteAll()
        {
            connection.ExecuteAsync("Delete from Message");

        }
    }
}
