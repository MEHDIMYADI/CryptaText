// ==================================================
//  CryptaText - CryptoService.cs
//  Author: MEHDIMYADI
//  GitHub: https://github.com/MEHDIMYADI
//  Copyright © 2026 MEHDIMYADI. All rights reserved.
// ==================================================
using System.Security.Cryptography;
using System.Text;

namespace CryptaText.Services
{
    public static class CryptoService
    {
        private static readonly byte[] Salt =
            Encoding.UTF8.GetBytes("CryptaText_Salt_2026");

        public static string Encrypt(string plainText, string key)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                return string.Empty;

            key = string.IsNullOrWhiteSpace(key) ? "CryptaText" : key;

            using var kdf = new Rfc2898DeriveBytes(key, Salt, 15000, HashAlgorithmName.SHA256);
            using var aes = Aes.Create();

            aes.Key = kdf.GetBytes(32);
            aes.IV = kdf.GetBytes(16);

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);

            var data = Encoding.UTF8.GetBytes(plainText);
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText, string key)
        {
            if (string.IsNullOrWhiteSpace(cipherText))
                return string.Empty;

            key = string.IsNullOrWhiteSpace(key) ? "CryptaText" : key;

            using var kdf = new Rfc2898DeriveBytes(key, Salt, 15000, HashAlgorithmName.SHA256);
            using var aes = Aes.Create();

            aes.Key = kdf.GetBytes(32);
            aes.IV = kdf.GetBytes(16);

            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var sr = new StreamReader(cs, Encoding.UTF8);

            return sr.ReadToEnd();
        }
    }
}