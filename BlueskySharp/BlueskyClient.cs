using BlueskySharp.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

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

        public async Task AuthentifiateAsync(string identifier, string password)
        {
            var auth = new AuthRequest()
            {
                Identifier = identifier,
                Password = password
            };
            var resp = await HttpClient.PostAsJsonAsync($"{_url}/xrpc/com.atproto.server.createSession", auth, _options);
            if (!resp.IsSuccessStatusCode)
            {
                var msg = JsonSerializer.Deserialize<ErrorResponse>(await resp.Content.ReadAsStringAsync(), _options).Message;
                throw new Exception(msg);
            }
            var res = JsonSerializer.Deserialize<AuthResponse>(await resp.Content.ReadAsStringAsync(), _options);
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", res.AccessJwt);
        }

        public async Task<Profile> GetProfileAsync(string actor)
        {
            var resp = await HttpClient.GetAsync($"{_url}/xrpc/app.bsky.actor.getProfile?actor={HttpUtility.UrlEncode(actor)}");
            if (!resp.IsSuccessStatusCode)
            {
                var msg = JsonSerializer.Deserialize<ErrorResponse>(await resp.Content.ReadAsStringAsync(), _options).Message;
                throw new Exception(msg);
            }
            return JsonSerializer.Deserialize<Profile>(await resp.Content.ReadAsStringAsync(), _options);
        }

        public async Task<Timeline> GetTimelineAsync()
        {
            var resp = await HttpClient.GetAsync($"{_url}/xrpc/app.bsky.feed.getTimeline");
            if (!resp.IsSuccessStatusCode)
            {
                var msg = JsonSerializer.Deserialize<ErrorResponse>(await resp.Content.ReadAsStringAsync(), _options).Message;
                throw new Exception(msg);
            }
            return JsonSerializer.Deserialize<Timeline>(await resp.Content.ReadAsStringAsync(), _options);
        }

        ~BlueskyClient()
        {
            HttpClient.Dispose();
        }

        public HttpClient HttpClient { set; private get; } = new();
        private JsonSerializerOptions _options;
        private readonly string _url;
    }

    public record ErrorResponse
    {
        public string Error { set; get; }
        public string Message { set; get; }
    }
}