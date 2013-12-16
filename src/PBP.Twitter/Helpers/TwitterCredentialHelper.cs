using System;
using System.Web;

namespace PBP.Twitter
{
    public class TwitterCredentialHelper
    {
        /// <summary>
        /// Creates an application-only encoded authentication token needed to
        /// authenticate with and send requests to the twitter REST API
        /// See: https://dev.twitter.com/docs/auth/application-only-auth
        /// </summary>
        public static string GenerateToken(string consumerKey, string consumerSecret)
        {
            var keyUrlEncoded = HttpUtility.UrlEncode(consumerKey);
            var secretUrlEncoded = HttpUtility.UrlEncode(consumerSecret);

            var bearerTokenCredentials = CreateBearerTokenCredentials(keyUrlEncoded, secretUrlEncoded);

            var bearerTokenCredentialsEncoded = EncodeBearerToken(bearerTokenCredentials);

            return bearerTokenCredentialsEncoded;
        }

        private static string EncodeBearerToken(string bearerTokenCredentials)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(bearerTokenCredentials);
            return Convert.ToBase64String(plainTextBytes);
        }

        private static string CreateBearerTokenCredentials(string keyUrlEncoded, string secretUrlEncoded)
        {
            return string.Format("{0}:{1}", keyUrlEncoded, secretUrlEncoded);
        }
    }
}