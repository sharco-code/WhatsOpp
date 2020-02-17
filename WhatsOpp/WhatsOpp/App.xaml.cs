using SQLite;
using System;
using WhatsOpp.Config;
using WhatsOpp.Model;
using WhatsOpp.Model.Local;
using WhatsOpp.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOpp {
    public partial class App : Application {

        private SQLiteAsyncConnection connection;
        public App()
        {
            InitializeComponent();

            //MainPage = new InitView();
            MainPage = new NavigationPage(new InitView());
        }

        protected override void OnStart()
        {
            //Crear o cargar las tablas de base de datos a partir de los modelos
            
            connection = new SQLiteAsyncConnection(Cfg.Database);

            connection.CreateTableAsync<Profile>().Wait();
            connection.CreateTableAsync<Chat>().Wait();
            connection.CreateTableAsync<Contact>().Wait();
            connection.CreateTableAsync<Message>().Wait();
            connection.CreateTableAsync<Configuration>().Wait();
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
