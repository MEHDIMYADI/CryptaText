// ==================================================
//  CryptaText - MainPage.xaml.cs
//  Author: MEHDIMYADI
//  GitHub: https://github.com/MEHDIMYADI
//  Copyright © 2026 MEHDIMYADI. All rights reserved.
// ==================================================
using CryptaText.Resources.Strings;
using CryptaText.Services;
using System.Globalization;

namespace CryptaText
{
    public partial class MainPage : ContentPage
    {
        private bool isKeyVisible = false;

        public MainPage()
        {
            InitializeComponent();

            KeyText.Text = SettingsService.LastKey;

            ApplyThemePickerItems();
            ChangeLanguage(SettingsService.Language);
            ApplyTheme();
        }

        void EncryptClicked(object sender, EventArgs e)
        {
            string key = KeyText.Text ?? "";
            SettingsService.LastKey = key;
            SettingsService.Save();

            OutputText.Text = CryptoService.Encrypt(InputText.Text ?? "", key);
        }

        async void DecryptClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputText.Text) || string.IsNullOrWhiteSpace(KeyText.Text))
            {
                await DisplayAlertAsync(
                    AppStrings.ErrorTitle,
                    "Input or Key cannot be empty.",
                    AppStrings.Ok
                );
                return;
            }

            try
            {
                OutputText.Text = CryptoService.Decrypt(InputText.Text!, KeyText.Text!);
            }
            catch
            {
                await DisplayAlertAsync(
                    AppStrings.ErrorTitle,
                    AppStrings.ErrorMessage,
                    AppStrings.Ok
                );
            }
        }

        void SetFa(object sender, EventArgs e) => ChangeLanguage("fa");
        void SetEn(object sender, EventArgs e) => ChangeLanguage("en");

        void ChangeLanguage(string lang)
        {
            SettingsService.Language = lang;
            SettingsService.Save();

            CultureInfo.CurrentUICulture = new CultureInfo(lang);
            //CultureInfo.CurrentCulture = new CultureInfo(lang);

            FlowDirection = lang == "fa" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

            ApplyLocalization();
        }

        void ApplyLocalization()
        {
            //Title = GetString("AppHeader");
            InputText.Placeholder = MainPage.GetString("InputPlaceholder");
            KeyText.Placeholder = MainPage.GetString("KeyPlaceholder");
            OutputText.Placeholder = MainPage.GetString("OutputPlaceholder");

            EncryptBtn.Text = MainPage.GetString("Encrypt");
            DecryptBtn.Text = MainPage.GetString("Decrypt");

            UpdateLangToggleButton();

            UpdateToggleKeyButton();

            ApplyThemePickerItems();
        }

        private void ToggleKeyClicked(object sender, EventArgs e)
        {
            isKeyVisible = !isKeyVisible;
            KeyText.IsPassword = !isKeyVisible;
            UpdateToggleKeyButton();
        }

        private void ToggleLanguageClicked(object sender, EventArgs e)
        {
            if (SettingsService.Language == "fa")
                ChangeLanguage("en");
            else
                ChangeLanguage("fa");

            UpdateLangToggleButton();
        }

        private void ApplyThemePickerItems()
        {
            ThemePicker.Items.Clear();
            ThemePicker.Items.Add(GetString("ThemeSystem"));
            ThemePicker.Items.Add(GetString("ThemeLight"));
            ThemePicker.Items.Add(GetString("ThemeDark"));

            ThemePicker.SelectedIndex = (int)SettingsService.ThemeMode;
        }

        private void ThemePickerChanged(object sender, EventArgs e)
        {
            if (ThemePicker.SelectedIndex == -1) return;

            SettingsService.ThemeMode = (AppThemeMode)ThemePicker.SelectedIndex;
            SettingsService.Save();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            switch (SettingsService.ThemeMode)
            {
                case AppThemeMode.System:
                    Application.Current!.UserAppTheme = AppTheme.Unspecified;
                    break;
                case AppThemeMode.Light:
                    Application.Current!.UserAppTheme = AppTheme.Light;
                    break;
                case AppThemeMode.Dark:
                    Application.Current!.UserAppTheme = AppTheme.Dark;
                    break;
            }
        }

        private void UpdateLangToggleButton()
        {
            LangToggleBtn.Text = SettingsService.Language == "fa"
                ? GetString("LangFa")
                : GetString("LangEn");
        }

        private void UpdateToggleKeyButton()
        {
            ToggleKeyBtn.Text = isKeyVisible ? "👁" : "🙈";
        }

        private void PasteInputClicked(object sender, EventArgs e)
        {
            if (Clipboard.HasText)
                InputText.Text = Clipboard.GetTextAsync().Result;
        }

        private void ClearInputClicked(object sender, EventArgs e)
        {
            InputText.Text = string.Empty;
        }

        private void CopyOutputClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OutputText.Text))
                Clipboard.SetTextAsync(OutputText.Text);
        }

        private void ClearOutputClicked(object sender, EventArgs e)
        {
            OutputText.Text = string.Empty;
        }

        static string GetString(string key)
        {
            return AppStrings.ResourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? key;
        }
    }
}