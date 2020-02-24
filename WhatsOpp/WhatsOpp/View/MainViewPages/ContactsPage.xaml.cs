using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOpp.DTO.Send;
using WhatsOpp.Model.Local;
using WhatsOpp.Utils;
using WhatsOpp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOpp.View.MainViewPages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage {
        public ContactsPage()
        {
            InitializeComponent();
            BindingContext = new ContactPageViewModel {
                AddContactSendDTO = new AddContactSendDTO()
            };
            ((ContactPageViewModel)BindingContext).refresh();
        }

        private async void ContactList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                ((ContactPageViewModel)BindingContext).sendAddChat((Contact)e.Item);
                await DisplayAlert("Info", "Chat añadido\nYa puedes empezara a chatear!", "Aceptar");
            }
            catch (MyException ex)
            {
                await DisplayAlert("Info", "No se ha podido añadir chat\nError: "+ex.Message, "Aceptar");
                return;
            }
        }

        private async void AddContact_Clicked(object sender, EventArgs e)
        {
            try
            {
                ((ContactPageViewModel)BindingContext).sendAddContact();
                await DisplayAlert("Info", "Contacto añadido", "Aceptar");
            }
            catch (MyException ex)
            {
                await DisplayAlert("Info", "No se ha podido añadir contacto\nError: "+ex.Message, "Aceptar");
                return;
            }
        }
    }
}