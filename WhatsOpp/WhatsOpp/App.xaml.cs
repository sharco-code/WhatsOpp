using SQLite;
using System;
using WhatsOpp.Config;
using WhatsOpp.DAO.Local;
using WhatsOpp.DTO.Send;
using WhatsOpp.Model;
using WhatsOpp.Model.Local;
using WhatsOpp.View;
using WhatsOpp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOpp {
    public partial class App : Application {

        private SQLiteAsyncConnection connection;
        private ConfigurationDAO configurationDAO = new ConfigurationDAO(Cfg.Database);
        private ProfileDAO profileDAO = new ProfileDAO(Cfg.Database);
        public App()
        {
            InitializeComponent();

            if(configurationDAO.isEmpty())
            {
                MainPage = new InitView();
                configurationDAO.Insert(new Configuration(1));
            } else
            {
                if(profileDAO.isEmpty())
                {
                    MainPage = new LoginView {
                        BindingContext = new LoginViewModel
                        {
                            LoginSendDTO = new LoginSendDTO()
                        }
                    };
                } else
                {
                    MainPage = new NavigationPage(new MainView());
                }
                
            }
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
