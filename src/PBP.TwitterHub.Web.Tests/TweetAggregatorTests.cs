using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PBP.Twitter;
using PBP.TwitterHud.Web;

namespace PBP.TwitterHub.Web.Tests
{
    [TestFixture]
    public class TweetAggregatorTests
    {
        private List<Tweet> _tweets;

        [SetUp]
        public void SetUp()
        {
            _tweets = new List<Tweet>();            
        }

        private void Given_tweets_from_multiple_users_with_mentions()
        {
            _tweets.Add(new Tweet
            {
                Text = "A funny tweet with a mention @nancy",
                TweetedAt = DateTime.Now,
                User = "bob"
            });

            _tweets.Add(new Tweet
            {
                Text = "A serious tweet with another mentioned @nancy",
                TweetedAt = DateTime.Now,
                User = "bob"
            });

            _tweets.Add(new Tweet
            {
                Text = "A tweet with multiple mentions @nancy @drew",
                TweetedAt = DateTime.Now,
                User = "bob"
            });

            _tweets.Add(new Tweet
            {
                Text = "A tweet from nancy with no mentions",
                TweetedAt = DateTime.Now,
                User = "nancy"
            });
        }

        [Test]
        public void TotalTweetsForBobShouldBe3()
        {
            Given_tweets_from_multiple_users_with_mentions();

            var result = TweetAggregator.Process(_tweets);

            result["bob"].totalTweets.Should().Be(3);
        }

        [Test]
        public void TotalUniqueMentionsByBobShouldBe2()
        {
            Given_tweets_from_multiple_users_with_mentions();

            var result = TweetAggregator.Process(_tweets);

            result["bob"].mentions.Keys.Count.Should().Be(2);
        }

        [Test]
        public void TotalMentionsOfNancyByBobShouldBe2()
        {
            Given_tweets_from_multiple_users_with_mentions();

            var result = TweetAggregator.Process(_tweets);

            result["bob"].mentions["nancy"].Should().Be(2);
        }

        [Test]
        public void TotalMentionsOfDrewByBobShouldBe1()
        {
            Given_tweets_from_multiple_users_with_mentions();

            var result = TweetAggregator.Process(_tweets);

            result["bob"].mentions["drew"].Should().Be(1);
        }

        [Test]
        public void TotalTweetsForNancyShouldBe1()
        {
            Given_tweets_from_multiple_users_with_mentions();

            var result = TweetAggregator.Process(_tweets);

            result["nancy"].totalTweets.Should().Be(1);
        }

        [Test]
        public void TotalUniqueMentionsByNancyShouldBe0()
        {
            Given_tweets_from_multiple_users_with_mentions();

            var result = TweetAggregator.Process(_tweets);

            result["nancy"].mentions.Count.Should().Be(0);
        }
    }
}
    