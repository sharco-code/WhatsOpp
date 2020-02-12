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

        private Boolean isNumber(String s)
        {
            try
            {
                int num = int.Parse(s);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async void Singin_Clicked(object sender, EventArgs e)
        {
            if(!isNumber(EntryPhone.Text))
            {
                await DisplayAlert("Info", "El numero tiene que ser un valor numerico", "Aceptar");
                return;
            }
            try
            {
                ((SinginViewModel)BindingContext).SendSingin();
                await DisplayAlert("Enorabuena!", "Te has registrado satisfactoriamente!\n\n Ahora, pulsa aceptar para iniciar sesión", "Aceptar");
                Login_Clicked(null, null);
            } 
            catch (MyException ex)
            {
                String msg = "";
                switch (ex.Code)
                {
                    case Error.USER_ALREADY_EXIST:
                        msg = "Ese nombre usuario ya existe.";
                        break;
                    case Error.NO_NAME:
                        msg = "Nombre requerido.";
                        break;
                    case Error.NO_PASSWORD:
                        msg = "Contraseña requerido.";
                        break;
                    case Error.NO_PHONE:
                        msg = "Telefono requerido.";
                        break;
                    case Error.NO_EMAIL:
                        msg = "Email requerido.";
                        break;
                    case Error.NO_USERNAME:
                        msg = "Nombre de usuario requerido.";
                        break;
                    default:
                        Console.WriteLine("Error inesperado");
                        break;
                }

                await DisplayAlert("Info", msg, "Aceptar");
                return;
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