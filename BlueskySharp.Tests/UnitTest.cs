using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueskySharp.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async Task TestGetPosts()
        {
            string identifier = Environment.GetEnvironmentVariable("BLUESKY_IDENTIFIER")!;
            string password = Environment.GetEnvironmentVariable("BLUESKY_PASSWORD")!;

            var client = new BlueskyClient();
            await client.AuthentifiateAsync(identifier, password);

            var p = await client.GetProfile("theindra.nl");

            Assert.AreEqual("theindra.nl", p.Handle);
            Assert.AreEqual("Indra", p.DisplayName);
            Assert.IsNotNull(p.Description);
            Assert.IsNotNull(p.Avatar);
            Assert.IsNotNull(p.Banner);
            Assert.IsTrue(p.FollowersCount > 0);
            Assert.IsTrue(p.FollowsCount > 0);
            Assert.IsTrue(p.PostsCount > 0);
            Assert.AreEqual(new DateTime(2023, 7, 4, 15, 30, 18, 756), p.IndexedAt);
            Assert.IsNotNull(p.Viewer);
            Assert.IsFalse(p.Viewer.Muted);
            Assert.IsFalse(p.Viewer.BlockedBy);
        }
    }
}