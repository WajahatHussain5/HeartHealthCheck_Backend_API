using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Dr_Heart_Specialist_api.Models
{
    public class Hash
    {
        public String CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        public String GenerateSha256Hash(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed shaString = new SHA256Managed();
            byte[] hash = shaString.ComputeHash(bytes);

            return BitConverter.ToString(hash);
        }
    }
}