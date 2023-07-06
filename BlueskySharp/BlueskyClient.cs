using BlueskySharp.Exceptions;
using BlueskySharp.Response;
using BlueskySharp.Response.Error;
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

        public async Task AuthenticateAsync(string identifier, string password, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(identifier);
            ArgumentNullException.ThrowIfNull(password);

            var auth = new AuthRequest()
            {
                Identifier = identifier,
                Password = password
            };

            var resp = await HttpClient.PostAsJsonAsync($"{_url}/xrpc/com.atproto.server.createSession", auth, _options, cancellationToken);

            if (!resp.IsSuccessStatusCode)
            {
                // Read the error response
                var error = await resp.Content.ReadFromJsonAsync<ErrorResponse>(_options, cancellationToken);

                throw new BlueskyException(error.Message, resp.StatusCode);
            }

            var res = await resp.Content.ReadFromJsonAsync<AuthResponse>(_options, cancellationToken);

            // Set the default Authorization header on further requests
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", res.AccessJwt);
        }

        public async Task<Profile> GetProfileAsync(string actor, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(actor);

            var resp = await HttpClient.GetAsync($"{_url}/xrpc/app.bsky.actor.getProfile?actor={HttpUtility.UrlEncode(actor)}", cancellationToken);

            if (!resp.IsSuccessStatusCode)
            {
                // Read the error response
                var error = await resp.Content.ReadFromJsonAsync<ErrorResponse>(_options, cancellationToken);

                throw new BlueskyException(error.Message, resp.StatusCode);
            }

            return await resp.Content.ReadFromJsonAsync<Profile>(_options, cancellationToken);
        }

        public async Task<Timeline> GetTimelineAsync(CancellationToken cancellationToken = default)
        {
            var resp = await HttpClient.GetAsync($"{_url}/xrpc/app.bsky.feed.getTimeline", cancellationToken);

            if (!resp.IsSuccessStatusCode)
            {
                // Read the error response
                var error = await resp.Content.ReadFromJsonAsync<ErrorResponse>(_options, cancellationToken);

                throw new BlueskyException(error.Message, resp.StatusCode);
            }

            return await resp.Content.ReadFromJsonAsync<Timeline>(_options, cancellationToken);
        }

        ~BlueskyClient()
        {
            HttpClient.Dispose();
        }

        public HttpClient HttpClient { set; private get; } = new();
        private JsonSerializerOptions _options;
        private readonly string _url;
    }
}