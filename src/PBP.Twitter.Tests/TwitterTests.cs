using System;
using System.Linq;
using System.Security.Authentication;
using FluentAssertions;
using NUnit.Framework;
using PBP.Twitter.Tests.Properties;

namespace PBP.Twitter.Tests
{
    [TestFixture]
    public class TwitterTests
    {
        private ITwitter _twitter;
        private string _user;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _twitter = new Twitter(Settings.Default.ConsumerKey, Settings.Default.ConsumerSecret);
            _user = "PayByPhone";            
        }

        [Test]
        public void SearchForUsersTweetsShouldReturnTweets()
        {
            _twitter.Search(string.Format("from:{0}", _user))
                .Count()
                .Should()
                .BeGreaterThan(0);
        }

        [Test]
        public void SearchForUsersTweetsSinceOneYearAgoShouldReturnTweets()
        {
            var oneYearAgoFormatted = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");

            _twitter.Search(string.Format("from:{0} since:{1}", _user, oneYearAgoFormatted))
                .Count()
                .Should()
                .BeGreaterThan(0);
        }

        [Test]
        public void SearchForUsersTweetsShouldReturnTweetsWithText()
        {
            _twitter.Search(string.Format("from:{0}", _user))
                .Should()
                .OnlyContain(tweet => !string.IsNullOrEmpty(tweet.Text));
        }

        [Test]
        public void SearchForUsersTweetsSinceOneYearAgoShouldReturnTweetsWithDateTime()
        {
            var oneYearAgo = DateTime.Now.AddYears(-1);
            var oneYearAgoFormatted = oneYearAgo.ToString("yyyy-MM-dd");

            _twitter.Search(string.Format("from:{0} since:{1}", _user, oneYearAgoFormatted))
                .Should()
                .OnlyContain(tweet =>
                    DateTime.Compare(tweet.TweetedAt, oneYearAgo) > 0
                );
        }

        [Test]
        [ExpectedException(typeof(AuthenticationException))]
        public void TwitterWhenInstantiatingWithBadCredentialsShouldThrowAuthenicationException()
        {
            var t= new Twitter("bad key", "bad secret");
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void TwitterWhenInstantiatingGivenNullConsumerKeyShouldThrowArgumentNullException()
        {
            var t = new Twitter(null, Settings.Default.ConsumerSecret);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void TwitterWhenInstantiatingGivenNullConsumerSecretShouldThrowArgumentNullException()
        {
            var t = new Twitter(Settings.Default.ConsumerKey, null);
        }
    }
}
