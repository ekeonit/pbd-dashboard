using System;

namespace PBP.Twitter.Models
{
    public class Tweet
    {
        public string Text { get; set; }
        public DateTime TweetedAt { get; set; }
        public string User { get; set; }
    }
}