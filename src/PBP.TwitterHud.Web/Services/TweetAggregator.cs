using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PBP.Twitter.Models;

namespace PBP.TwitterHud.Web.Services
{
    public class TweetAggregator
    {
        private const string MentionPattern = @"@\w+";

        public class TweetAggregatorResult
        {
            public Dictionary<string, int> Mentions { get; set; }
            public int TotalTweets { get; set; }
        }

        public static Dictionary<string, TweetAggregatorResult> Process(List<Tweet> tweets)
        {
            var processed = tweets
                .GroupBy(tweet => tweet.User)
                .ToDictionary(grouping => grouping.Key, grouping => new TweetAggregatorResult()
                {
                    TotalTweets = grouping.Count(),
                    Mentions = ProcessMentions(tweets.Where(tweet => tweet.User == grouping.Key))
                });

            return processed;
        }

        private static Dictionary<string, int> ProcessMentions(IEnumerable<Tweet> tweets)
        {
            var mentions = new Dictionary<string, int>();

            // the following query will iterate over each usermentioned in the
            // given collection of tweets, stripping off the '@' character,
            // tabulating the number of total mentions

            foreach (
                var user in tweets.SelectMany(tweet => (from Match match in Regex.Matches(tweet.Text, MentionPattern)
                    select match.Value.Remove(0, 1))))
            {
                if (mentions.ContainsKey(user))
                {
                    mentions[user]++;
                }
                else
                {
                    mentions.Add(user, 1);
                }
            }

            return mentions;
        }
    }
}