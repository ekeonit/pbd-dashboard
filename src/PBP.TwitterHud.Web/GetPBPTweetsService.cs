using System;
using System.Collections.Generic;
using PBP.Twitter;

namespace PBP.TwitterHud.Web
{
    public class GetPBPTweetsService : IGetPBPTweetsService
    {
        public GetPBPTweetsService(string[] accounts, ITwitter twitter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PBPTweet> GeTweetsSince(DateTime sinceDateTime)
        {
            throw new NotImplementedException();
        }
    }
}