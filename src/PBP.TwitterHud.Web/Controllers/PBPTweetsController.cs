using System;
using System.Linq;
using System.Web.Mvc;
using PBP.TwitterHud.Web.Models;

namespace PBP.TwitterHud.Web.Controllers
{
    public class PBPTweetsController : Controller
    {
        private readonly IGetPBPTweetsService _getPBPTweetsService;

        public PBPTweetsController(IGetPBPTweetsService getPBPTweetsService)
        {
            _getPBPTweetsService = getPBPTweetsService;
        }

        [HttpGet]
        public JsonResult Get(DateTime sinceDateTime)
        {
            var tweets = _getPBPTweetsService.GeTweetsSince(sinceDateTime).ToList();

            var response = new GetPBPTweetsResponseModel
            {
                tweets = tweets.OrderByDescending(tweet => tweet.TweetedAt)
                    .Select(tweet =>
                        new GetPBPTweetsResponseModel.Tweet
                        {
                            text = tweet.Text,
                            tweetedAt = tweet.TweetedAt,
                            user = tweet.User
                        })
                    .ToArray(),

                userAggregateData = TweetAggregator.Process(tweets)
                    .ToDictionary(pair => pair.Key, pair =>
                        new GetPBPTweetsResponseModel.UserAggregateData
                        {
                            mentions = pair.Value.Mentions,
                            totalTweets = pair.Value.TotalTweets
                        })
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}