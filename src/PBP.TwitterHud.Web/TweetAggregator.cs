using System;
using System.Collections.Generic;
using PBP.Twitter;

namespace PBP.TwitterHud.Web
{
    public class TweetAggregator
    {
        public class TweetAggregatorResult
        {
            public Dictionary<string, int> mentions { get; set; }
            public int totalTweets { get; set; }
        }

        public static Dictionary<string, TweetAggregatorResult> Process(List<Tweet> tweets)
        {
            throw new NotImplementedException();
        }
    }
}