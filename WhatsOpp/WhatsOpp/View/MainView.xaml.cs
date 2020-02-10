using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOpp.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : TabbedPage {
        public MainView()
        {
            InitializeComponent();
        }

        private void ChatsPage_Appearing(object sender, EventArgs e)
        {

        }
    }
}