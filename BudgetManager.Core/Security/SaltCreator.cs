using System;
using System.Security.Cryptography;

namespace BudgetManager.Core.Security
{
    public class SaltCreator
    {
        public static string CreateRandomSalt()
        {
            byte[] salt = new byte[32];

            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
    }
}