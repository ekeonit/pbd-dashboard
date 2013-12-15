using System.Collections.Generic;

namespace PBP.Twitter
{
    public interface ITwitter
    {
        IEnumerable<Tweet> Search(string query);
    }
}