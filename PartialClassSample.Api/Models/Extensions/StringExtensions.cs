using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace PartialClassSample.Api.Models.Extensions
{
    public static class StringExtensions
    {
        public static string Encrypt (this string password)
        {
            var encoding = new UnicodeEncoding();

            byte[] hashBytes = SHA1.Create().ComputeHash(encoding.GetBytes(password));

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);

            foreach (byte b in hashBytes)
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);

            return hashValue.ToString();
        }
    }
}
