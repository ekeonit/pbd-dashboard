using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Web;
using Newtonsoft.Json;

namespace PBP.Twitter
{
    public class Twitter : ITwitter
    {
        private readonly string _accessToken;

        internal class SearchResponse
        {
            internal class Status
            {
                public string text { get; set; }
                public string created_at { get; set; }
            }

            public Status[] statuses { get; set; }
        }

        internal class TwitterAuthResponse
        {
            public string token_type { get; set; }
            public string access_token { get; set; }
        }

        public Twitter(string consumerKey, string consumerSecret)
        {
            var encodedCredentials = TwitterCredentialHelper.GenerateToken(consumerKey, consumerSecret);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", string.Format("Basic {0}", encodedCredentials));

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    Content = new StringContent("grant_type=client_credentials"),
                    RequestUri = new Uri("https://api.twitter.com/oauth2/token"),
                };

                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = httpClient.SendAsync(request);

                if (!response.Result.IsSuccessStatusCode)
                {
                    throw new AuthenticationException();
                }

                var authResponse =
                    JsonConvert.DeserializeObject<TwitterAuthResponse>(
                        response.Result.Content.ReadAsStringAsync().Result);

                _accessToken = authResponse.access_token;
            }
        }

        public IEnumerable<Tweet> Search(string query)
        {
            using (var httpClient = new HttpClient())
            {
                var uriBuilder = new UriBuilder("https://api.twitter.com/1.1/search/tweets.json");
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["q"] = query;
                uriBuilder.Query = queryString.ToString();

                httpClient.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", _accessToken));
                var response = httpClient.GetStringAsync(uriBuilder.Uri);



                var searchResult = JsonConvert.DeserializeObject<SearchResponse>(response.Result);

                return searchResult.statuses.Select(status =>
                    new Tweet
                    {
                        Text = status.text,
                        TweetedAt = DateTime.ParseExact(status.created_at, "ddd MMM d HH:mm:ss K yyyy", CultureInfo.InvariantCulture)
                    });
            }
        }
    }
}