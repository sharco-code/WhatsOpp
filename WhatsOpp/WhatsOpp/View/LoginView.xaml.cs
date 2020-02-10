using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOpp.DTO;
using WhatsOpp.Utils;
using WhatsOpp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOpp.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage {
        public LoginView()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            try
            {
                ((LoginViewModel)BindingContext).SendLogin();
            }
            catch (MyException ex)
            {
                await DisplayAlert("Info", ex.Message, "Aceptar");
            }
        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SinginView
            {
                BindingContext = new SinginViewModel
                {
                    SinginDTO = new SinginDTO()
                }
            });
        }
    }
}