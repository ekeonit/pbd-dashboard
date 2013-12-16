using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PBP.Twitter;

namespace PBP.TwitterHud.Web
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
            var q = tweets.GroupBy(tweet => tweet.User)
                .ToDictionary(grouping => grouping.Key, grouping => new TweetAggregatorResult()
                {
                    TotalTweets = grouping.Count(),
                    Mentions = new Dictionary<string, int>()
                });

            // iterate through each tweet and extract matches for mentions
            // to build our dictionary

            foreach (var user in q.Keys)
            {
                var user1 = user;

                foreach (var tweet in tweets.Where(tweet => tweet.User == user1))
                {
                    foreach (Match match in Regex.Matches(tweet.Text, MentionPattern))
                    {
                        var stripAt = match.Value.Remove(0, 1);

                        if (q[user].Mentions.ContainsKey(stripAt))
                        {
                            q[user].Mentions[stripAt]++;
                        }
                        else
                        {
                            q[user].Mentions.Add(stripAt, 1);
                        }
                    }
                }
            }

            return q;
        }
    }
}