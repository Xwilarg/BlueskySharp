using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueskySharp.Tests
{
    [TestClass]
    public class AuthTests
    {
        private BlueskyClient _client;

        public AuthTests()
        {
            _client = new BlueskyClient();
        }

        [TestMethod]
        public async Task TestAuthenticate()
        {
            string identifier = Environment.GetEnvironmentVariable("BLUESKY_IDENTIFIER")!;
            string password = Environment.GetEnvironmentVariable("BLUESKY_PASSWORD")!;

            await _client.AuthenticateAsync(identifier, password);
        }
    }
}
