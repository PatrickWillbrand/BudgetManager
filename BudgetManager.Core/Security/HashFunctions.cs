using System.Security.Cryptography;
using System.Text;

namespace BudgetManager.Core.Security
{
    public class HashFunctions
    {
        public static string CreateSHA256(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(bytes);
                string hashString = string.Empty;

                foreach (var b in hash)
                {
                    hashString += string.Format("{0:x2}", b);
                }

                return hashString;
            }
        }
    }
}