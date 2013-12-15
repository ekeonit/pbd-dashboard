using System;
using System.Collections.Generic;

namespace PBP.TwitterHud.Web
{
    public interface IGetPBPTweetsService
    {
        IEnumerable<PBPTweet> GeTweetsSince(DateTime sinceDateTime);
    }
}