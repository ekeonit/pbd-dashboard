using System;
using System.Collections.Generic;

namespace PBP.TwitterHud.Web.Models
{
    public class GetPBPTweetsResponseModel
    {
        public Dictionary<string, UserAggregateData> userAggregateData { get; set; }
        public Tweet[] tweets { get; set; }

        public class Tweet
        {
            public string user { get; set; }
            public string text { get; set; }
            public string tweetedAt { get; set; }
        }

        public class UserAggregateData
        {
            public Dictionary<string, int> mentions;
            public int totalTweets { get; set; }
        }
    }
}