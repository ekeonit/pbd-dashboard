using System;
using System.Collections.Generic;
using PBP.Twitter.Models;

namespace PBP.TwitterHud.Web.Services
{
    public interface IGetPBPTweetsService
    {
        IEnumerable<Tweet> GeTweetsSince(DateTime sinceDateTime);
    }
}