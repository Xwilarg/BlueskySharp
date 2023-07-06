using System.Text;
using System.Text.Json;

namespace BlueskySharp
{
    public class BlueskyClient
    {
        public BlueskyClient(string domain = "bsky.social")
        {
            _url = $"https://{domain}";
        }

        public async Task AuthentificateAsync(string identifier, string password)
        {
            var auth = new Auth()
            {
                identifier = identifier,
                password = password
            };
            var resp = await HttpClient.PostAsync($"{_url}/xrpc/com.atproto.server.createSession", new StringContent(JsonSerializer.Serialize(auth), Encoding.UTF8, "application/json"));
            var res = await resp.Content.ReadAsStringAsync();
            Console.WriteLine(res);
            if (!resp.IsSuccessStatusCode)
            {
                throw new NotImplementedException();
            }
        }

        ~BlueskyClient()
        {
            HttpClient.Dispose();
        }

        public HttpClient HttpClient { private set; get; } = new();
        private string _url;

        internal record Auth
        {
            public string identifier { set; get; }
            public string password { set; get; }
        }
    }
}