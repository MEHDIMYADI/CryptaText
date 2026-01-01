// ==================================================
//  CryptaText - App.xaml.cs
//  Author: MEHDIMYADI
//  GitHub: https://github.com/MEHDIMYADI
//  Copyright © 2026 MEHDIMYADI. All rights reserved.
// ==================================================
using CryptaText.Services;

namespace CryptaText
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SettingsService.Load();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var mainPage = new NavigationPage(new MainPage());
            return new Window(mainPage);
        }
    }
}


