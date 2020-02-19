using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOpp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOpp.View.MainViewPages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage {
        public ContactsPage()
        {
            InitializeComponent();
            BindingContext = new ContactPageViewModel();
        }

        private void ContactList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private async void AddContact_Clicked(object sender, EventArgs e)
        {

        }
    }
}