// Copyright (c) 2018 Fox Council - License: MIT - https://github.com/FoxCouncil/FoxHue

using System;
using System.Text;
using System.Security.Cryptography;

namespace FoxHue
{
    internal static class FoxHueExtensions
    {
        public static string Protect(this string clearText, string optionalEntropy = null, DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            if (clearText == null)
            {
                throw new ArgumentNullException("clearText");
            }

            var clearBytes = Encoding.UTF8.GetBytes(clearText);
            var entropyBytes = string.IsNullOrEmpty(optionalEntropy) ? null : Encoding.UTF8.GetBytes(optionalEntropy);
            var encryptedBytes = ProtectedData.Protect(clearBytes, entropyBytes, scope);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Unprotect(this string encryptedText, string optionalEntropy = null, DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            if (encryptedText == null)
            {
                throw new ArgumentNullException("encryptedText");
            }

            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var entropyBytes = string.IsNullOrEmpty(optionalEntropy) ? null : Encoding.UTF8.GetBytes(optionalEntropy);
            var clearBytes = ProtectedData.Unprotect(encryptedBytes, entropyBytes, scope);

            return Encoding.UTF8.GetString(clearBytes);
        }
    }
}
