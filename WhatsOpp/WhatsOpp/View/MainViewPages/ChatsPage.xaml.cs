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
    public partial class ChatsPage : ContentPage {
        public ChatsPage()
        {
            InitializeComponent();
            BindingContext = new ChatsPageViewModel();
        }

        private void ChatList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}