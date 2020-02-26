using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOpp.Model.Local;
using WhatsOpp.Utils;
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

        protected override void OnAppearing()
        {
            ((ChatsPageViewModel)BindingContext).refresh();
        }

        private async void ChatList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ChatView
                {
                    BindingContext = new ChatViewModel
                    {
                        chat = (Chat)e.Item
                    }
                }); ;
            }
            catch (MyException ex)
            {
                await DisplayAlert("Info", ex.Message, "Aceptar");
                return;
            }

        }
    }
}