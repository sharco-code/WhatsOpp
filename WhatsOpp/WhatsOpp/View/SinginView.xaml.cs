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
    public partial class SinginView : ContentPage {


        public SinginView()
        {
            InitializeComponent();
        }

        private async void Singin_Clicked(object sender, EventArgs e)
        {
            
            try
            {
                ((SinginViewModel)BindingContext).SendSingin();
            } catch (MyException ex)
            {
                await DisplayAlert("Info", ex.Message, "Aceptar");
            }
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginView
            {
                BindingContext = new LoginViewModel
                {
                    LoginDTO = new LoginDTO()
                }
            });
        }
    }
}