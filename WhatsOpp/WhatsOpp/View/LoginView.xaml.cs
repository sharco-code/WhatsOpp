﻿using System;
using WhatsOpp.DTO.Send;
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
                App.Current.MainPage = new NavigationPage(new MainView());
            }
            catch (MyException ex)
            {
                await DisplayAlert("Info", ex.Message, "Aceptar");
                return;
            }

        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new SinginView
            {
                BindingContext = new SinginViewModel
                {
                    SinginSendDTO = new SinginSendDTO()
                }
            };
            /*
             * 
            Navigation.PushAsync(new SinginView
            {
                BindingContext = new SinginViewModel
                {
                    SinginSendDTO = new SinginSendDTO()
                }
            });
            */
        }
    }
}