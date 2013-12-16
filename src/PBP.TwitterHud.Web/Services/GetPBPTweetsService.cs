using System;
using System.Collections.Generic;
using System.Linq;
using PBP.Twitter;
using PBP.Twitter.Models;

namespace PBP.TwitterHud.Web.Services
{
    public class GetPBPTweetsService : IGetPBPTweetsService
    {
        private readonly string[] _users;
        private readonly ITwitter _twitter;

        public GetPBPTweetsService(string[] users, ITwitter twitter)
        {
            _users = users;
            _twitter = twitter;
        }

        public IEnumerable<Tweet> GeTweetsSince(DateTime sinceDateTime)
        {
            var tweets = new List<Tweet>();

            foreach (var user in _users)
            {
                var query = string.Format(
                    "{0} since:{1}", user, sinceDateTime.ToString("yyyy-MM-dd"));

                tweets.AddRange(_twitter.Search(query).Select(tweet => new Tweet
                {
                    Text = tweet.Text,
                    TweetedAt = tweet.TweetedAt,
                    User = user
                }));
            }

            return tweets;
        }
    }
}