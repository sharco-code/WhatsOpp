using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOpp.DTO;
using WhatsOpp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOpp.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitView : ContentPage {
        public InitView()
        {
            InitializeComponent();
        }

        private void Accept_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SinginView
            {
                BindingContext = new SinginViewModel
                {
                    SinginDTO = new SinginDTO()
                }
            }); 
        }

        private async void Policy_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Política de Privacidad", "La política de privacidad de WhatsOpp™ es muy estricta:\n\n\n\nAnexo I\n\nTodos tus datos personales podran ser utilizados de forma libre por WhatsOpp™ y esta está exenta de cualquier reclamación aplicable por el usuario ya que este está de acuerdo al aceptar nuestra politica de privacidad\n\n\nAnexo II\n\nTus datos podrán ser revelados al publico en cualquier momento aligual como vendidos a otras empresas\n\n\nAnexo III\n\nEn ningún momento puedes revocar los permisos concedidos a la aplicación para la utilización de tus estos", "Aceptar");
        }

        private async void Terms_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Terminos y Condiciones", "Al aceptar los Terminos y Condiciones de WhatsOpp™ cedes todas tus bienes presentes y futuros al propietario de la aplicación.\n\nAdemás, transcurridos 15 segundos del uso de esta aplicación se te aplicara un cargo de 4000 USD a tu cuenta corriente, en caso de no poder pagarlo, estarás endeudado hasta el pago del mismo.\n\nPara más información puedes llamar a nuestro servicio de atención al cliente llamando al 644226178 (Disp: 03:01-03:02)", "Aceptar");
        }

    }
}