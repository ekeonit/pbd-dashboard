using System;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using PBP.Twitter;
using PBP.TwitterHud.Web;
using PBP.TwitterHud.Web.Controllers;
using PBP.TwitterHud.Web.Models;
using Rhino.Mocks;
using TechTalk.SpecFlow;

namespace PBP.TwitterHud.AcceptanceTests.Features.Dashboard
{
    [Binding]
    public class DashboardSteps
    {
        private PBPTweetsController _controller;
        private ITwitter _twitter;
        private GetPBPTweetsResponseModel _response;

        [BeforeScenario]
        public void BeforeScenario()
        {
            var accounts = new[] {"pay_by_phone", "PayByPhone", "PayByPhone_UK"};

            _twitter = MockRepository.GenerateStub<ITwitter>();
            IGetPBPTweetsService getPBPTweetsService = new GetPBPTweetsService(accounts, _twitter);

            _controller = new PBPTweetsController(getPBPTweetsService);
        }

        [Given(@"the user '(.*)' has made the following tweets")]
        public void GivenTheUserHasMadeTheFollowingTweets(string userName, Table tweets)
        {
            _twitter
                .Stub(
                    twitter =>
                        twitter.Search(Arg<string>.Matches(s => Regex.IsMatch(s, string.Format("\\b{0}\\b", userName)))))
                .Return(tweets.Rows.Select(row =>
                    new Tweet
                    {
                        Text = row["Text"],
                        TweetedAt = DateTime.Parse(row["At"]),
                        User = userName
                    }));
        }

        [When(@"a request is received to see all tweets since 2 weeks before '(.*)'")]
        public void WhenARequestIsReceivedToSeeAllTweetsSince2WeeksBefore(DateTime sinceDateTime)
        {
            _response = _controller.Get(sinceDateTime.AddDays(-14)).Data as GetPBPTweetsResponseModel;
        }

        [Then(@"the total number of tweets for the account '(.*)' should be (.*)")]
        public void ThenTheTotalNumberOfTweetsForTheAccountShouldBe(string userName, int expectedTotalNumberOfTweets)
        {
            _response.userAggregateData[userName].totalTweets.Should().Be(expectedTotalNumberOfTweets);
        }

        [Then(@"the total number of times users were mentioned for the account '(.*)' should be")]
        public void ThenTheTotalNumberOfTimesUsersWereMentionedForTheAccountShouldBe(string userName, Table exptectedMentions)
        {
            exptectedMentions
                .Rows
                .ToList()
                .ForEach(
                    row =>
                        _response.userAggregateData[userName].mentions[row["user"]].Should()
                            .Be(int.Parse(row["number of mentions"])));
        }

        [Then(@"the sorted list of tweets for all of the accounts over the period should be")]
        public void ThenTheSortedListOfTweetsForAllOfTheAccountsOverThePeriodShouldBe(Table expectedTweets)
        {
            _response.tweets.Count().Should().Be(expectedTweets.Rows.Count);

            for (var i = 0; i < expectedTweets.Rows.Count; i++)
            {
                _response.tweets[i].user.Should().Be(expectedTweets.Rows[i]["user"]);
                _response.tweets[i].text.Should().Be(expectedTweets.Rows[i]["text"]);
                _response.tweets[i].tweetedAt.Should().Be(DateTime.Parse(expectedTweets.Rows[i]["at"]));
            }
        }
    }
}
