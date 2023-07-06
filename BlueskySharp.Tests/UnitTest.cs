using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueskySharp.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async Task TestLogin()
        {
            string identifier = Environment.GetEnvironmentVariable("BLUESKY_IDENTIFIER")!;
            string password = Environment.GetEnvironmentVariable("BLUESKY_PASSWORD")!;

            var client = new BlueskyClient();
            await client.AuthentificateAsync(identifier, password);
        }
    }
}