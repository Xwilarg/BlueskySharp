using System.Net.Http.Json;
using System.Text.Json;

namespace BlueskySharp
{
    public class BlueskyClient
    {
        public BlueskyClient(string domain = "bsky.social")
        {
            _url = $"https://{domain}";
            _options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AuthentificateAsync(string identifier, string password)
        {
            var auth = new Auth()
            {
                Identifier = identifier,
                Password = password
            };
            var resp = await HttpClient.PostAsJsonAsync($"{_url}/xrpc/com.atproto.server.createSession", auth, _options);
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
        private JsonSerializerOptions _options;
        private string _url;
    }

    internal record Auth
    {
        public string Identifier { set; get; }
        public string Password { set; get; }
    }
}