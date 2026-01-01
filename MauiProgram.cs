// ==================================================
//  CryptaText - MauiProgram.cs
//  Author: MEHDIMYADI
//  GitHub: https://github.com/MEHDIMYADI
//  Copyright © 2026 MEHDIMYADI. All rights reserved.
// ==================================================
namespace CryptaText
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>();
            return builder.Build();
        }
    }
}