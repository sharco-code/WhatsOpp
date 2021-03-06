﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WhatsOpp.Model.Local;

namespace WhatsOpp.DAO.Local {
    public class ContactDAO {
        private SQLiteAsyncConnection connection;
        public ContactDAO(string dbpath)
        {
            connection = new SQLiteAsyncConnection(dbpath);
        }
        internal void Insert(Contact contact)
        {
            if (contact == null) throw new Exception("ContactDAO [Insert] - OBJECT_NULL");
            connection.InsertAsync(contact);
        }
        internal void Save(Contact contact)
        {
            if (contact == null) throw new Exception("ContactDAO [Save] - OBJECT_NULL");
            connection.UpdateAsync(contact);
            //connection.InsertAsync(configuration);
        }
        internal void Delete(Contact contact)
        {
            if (contact == null) throw new Exception("ContactDAO [Delete] - OBJECT_NULL");
            connection.DeleteAsync(contact).Wait();
        }
        public ObservableCollection<Contact> GetContacts()
        {
            var l = connection.Table<Contact>().ToListAsync().Result;
            return new ObservableCollection<Contact>(l);
        }
        internal int CountRows()
        {
            List<Contact> result = connection.QueryAsync<Contact>("SELECT * FROM Contact").Result;
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
            connection.ExecuteAsync("Delete from Contact");

        }
    }
}
