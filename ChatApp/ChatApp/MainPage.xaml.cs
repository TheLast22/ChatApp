﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ChatApp_Leano_Stewart.ViewModels;
using ChatApp_Leano_Stewart.Helpers;

namespace ChatApp_Leano_Stewart
{
    public partial class MainPage : ContentPage
    {
        readonly Contacts contacts = new Contacts();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(this);
            NavigationPage.SetHasNavigationBar(this, false);
            Activity.IsVisible = false;

            EmailEntry.Focused += (s, a) =>
            {
                EmailFrame.BorderColor = Color.FromHex("#00529C");
            };

            PasswordEntry.Focused += (s, a) =>
            {
                PasswordFrame.BorderColor = Color.FromHex("#00529C");
            };
       
            
        }
        private async void SignIn_Clicked(object sender, EventArgs e)
        {   
            if(!string.IsNullOrEmpty(EmailEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text))
            {
                Activity.IsVisible = true;
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { };
                response = await DependencyService.Get<iFirebaseAuth>().LoginWithEmailPassword(EmailEntry.Text, PasswordEntry.Text);

                if(response.status == true)
                {
                    Application.Current.MainPage = new TabbedPage();
                }
                else
                {
                    bool retryBool = await DisplayAlert("Error", response.response, "Yes", "No");
                    if (retryBool)
                    {
                        EmailFrame.BorderColor = Color.Red;
                        PasswordFrame.BorderColor = Color.Red;
                        EmailEntry.Text = string.Empty;
                        PasswordEntry.Text = string.Empty;
                    }
                }
                Activity.IsVisible =false;
            } 
            else
            {
                EmailFrame.BorderColor = Color.Red;
                PasswordFrame.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing Fields. Please Enter Your Login Information.", "OKAY");    
                EmailEntry.Text = string.Empty;
                PasswordEntry.Text = string.Empty;
            }
        }
        private async void Register_Clicked(object sender, EventArgs e)
        {
           await Application.Current.SavePropertiesAsync();
           Application.Current.MainPage = new RegisterPage();
        }

        private void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ForgotPasswordPage());
        }

    }
}
