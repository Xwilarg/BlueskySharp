using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueskySharp.Tests
{
    [TestClass]
    public class PostTests
    {
        private BlueskyClient _client;

        public PostTests()
        {
            _client = new BlueskyClient();
        }

        [TestInitialize]
        public async Task Authenticate()
        {
            string identifier = Environment.GetEnvironmentVariable("BLUESKY_IDENTIFIER")!;
            string password = Environment.GetEnvironmentVariable("BLUESKY_PASSWORD")!;

            await _client.AuthenticateAsync(identifier, password);
        }

        [TestMethod]
        public async Task TestGetTimeline()
        {
            var t = await _client.GetTimelineAsync();

            Assert.IsTrue(t.Feed.Length > 0);
        }
    }
}
