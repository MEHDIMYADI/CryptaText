// ==================================================
//  CryptaText - SettingsService.cs
//  Author: MEHDIMYADI
//  GitHub: https://github.com/MEHDIMYADI
//  Copyright © 2026 MEHDIMYADI. All rights reserved.
// ==================================================
namespace CryptaText.Services
{
    public enum AppThemeMode
    {
        System,
        Light,
        Dark
    }

    public static class SettingsService
    {
        public static string Language { get; set; } = "fa";
        public static string LastKey { get; set; } = "";
        public static AppThemeMode ThemeMode { get; set; } = AppThemeMode.System;

        public static void Load()
        {
            Language = Preferences.Get("lang", "fa");
            LastKey = Preferences.Get("key", "");
            ThemeMode = (AppThemeMode)Preferences.Get("themeMode", (int)AppThemeMode.System);
        }

        public static void Save()
        {
            Preferences.Set("lang", Language);
            Preferences.Set("key", LastKey);
            Preferences.Set("themeMode", (int)ThemeMode);
        }
    }
}
