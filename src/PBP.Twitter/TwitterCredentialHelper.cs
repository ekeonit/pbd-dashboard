using System;
using System.Web;

namespace PBP.Twitter
{
    public class TwitterCredentialHelper
    {
        public static string GenerateToken(string consumerKey, string consumerSecret)
        {
            var keyUrlEncoded = HttpUtility.UrlEncode(consumerKey);
            var secretUrlEncoded = HttpUtility.UrlEncode(consumerSecret);
            var bearerTokenCredentials = string.Format("{0}:{1}", keyUrlEncoded, secretUrlEncoded);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(bearerTokenCredentials);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}