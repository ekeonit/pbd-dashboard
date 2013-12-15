using FluentAssertions;
using NUnit.Framework;

namespace PBP.Twitter.Tests
{
    [TestFixture]
    public class TwitterCredentialHelperTests
    {
        /// <summary>
        /// Tests based on example at: https://dev.twitter.com/docs/auth/application-only-auth
        /// </summary>
        [Test]
        public void GenerateTokenShouldReturnCorrectCredentials()
        {
            const string consumerKey = "xvz1evFS4wEEPTGEFPHBog";
            const string consumerSecret = "L8qq9PZyRg6ieKGEKhZolGC0vJWLw8iEJ88DRdyOg";
            const string expectedCredentials =
                "eHZ6MWV2RlM0d0VFUFRHRUZQSEJvZzpMOHFxOVBaeVJnNmllS0dFS2hab2xHQzB2SldMdzhpRUo4OERSZHlPZw==";

            TwitterCredentialHelper.GenerateToken(consumerKey, consumerSecret)
                .Should()
                .Be(expectedCredentials);
        }
    }
}
