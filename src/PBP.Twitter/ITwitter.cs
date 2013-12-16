using System.Collections.Generic;
using PBP.Twitter.Models;

namespace PBP.Twitter
{
    public interface ITwitter
    {
        IEnumerable<Tweet> Search(string query);
    }
}