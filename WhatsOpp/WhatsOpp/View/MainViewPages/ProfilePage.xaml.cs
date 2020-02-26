using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOpp.DTO.Send;
using WhatsOpp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOpp.View.MainViewPages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfilePageViewModel();
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            try
            {
                ((ProfilePageViewModel)BindingContext).LogOut();
                App.Current.MainPage = new LoginView
                {
                    BindingContext = new LoginViewModel
                    {
                        LoginSendDTO = new LoginSendDTO()
                    }
                };
            }
            catch(Exception x)
            {

            }
            
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Info", "Esta función estará disponible en futuras versiones", "Aceptar");
        }
    }
}