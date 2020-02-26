using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOpp.Model.Local;
using WhatsOpp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOpp.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView : ContentPage {
        public ChatView()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(8), () =>
            {
                Device.BeginInvokeOnMainThread(() => autoRefresh());
                return true;
            });
        }
        protected override void OnAppearing()
        {
            ((ChatViewModel)BindingContext).getLocalMessages();
            goToLast();

        }

        private void sendTextClicked(object sender, EventArgs e)
        {
          
            ((ChatViewModel)BindingContext).sendMessage(mensaje.Text);
            mensaje.Text = "";
            goToLast();
        }

        private void autoRefresh() {

            
            ((ChatViewModel)BindingContext).refresh();
            goToLast();

        }
        private void goToLast()
        {
            var v = listMessages.ItemsSource.Cast<object>().LastOrDefault();
            listMessages.ScrollTo(v, ScrollToPosition.End, false);
        }


    }
}