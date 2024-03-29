﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_Leano_Stewart
{
    public partial class App : Application
    {
        //UI SCALE
        public static float screenWidth { get; set; }
        public static float screenHeight { get; set; }
        public static float appScale { get; set; }
        public static string User = "Rendy";


        public App()
        {
            InitializeComponent();
            FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { };
            response = DependencyService.Get<iFirebaseAuth>().IsLoggedIn();
            if (!response.status)
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new TabbedPage();
            }     
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
