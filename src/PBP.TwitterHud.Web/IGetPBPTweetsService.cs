using System;
using System.Collections.Generic;
using PBP.Twitter;

namespace PBP.TwitterHud.Web
{
    public interface IGetPBPTweetsService
    {
        IEnumerable<Tweet> GeTweetsSince(DateTime sinceDateTime);
    }
}