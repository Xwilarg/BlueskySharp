namespace BlueskySharp.Response
{
    internal record AuthRequest
    {
        public string Identifier { set; get; }
        public string Password { set; get; }
    }
    internal record AuthResponse
    {
        public string AccessJwt { set; get; }
    }
}
