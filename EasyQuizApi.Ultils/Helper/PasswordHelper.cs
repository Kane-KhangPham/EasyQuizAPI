using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace EasyQuizApi.Share.Helper
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("X2"));
                }
                return sBuilder.ToString();
            }
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return hash.Equals(HashPassword(password));
        }
    }
}
