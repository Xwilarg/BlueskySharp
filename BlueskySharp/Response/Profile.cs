namespace BlueskySharp.Response
{
    public record Viewer
    {
        public bool Muted { set; get; }
        public bool BlockedBy { set; get; }
        public string Following { set; get; }
        public string FollowedBy { set; get; }
    }

    public record Profile
    {
        public string Handle { set; get; }
        public string DisplayName { set; get; }
        public string Description { set; get; }
        public string Avatar { set; get; }
        public string Banner { set; get; }
        public int FollowsCount { set; get; }
        public int FollowersCount { set; get; }
        public int PostsCount { set; get; }
        public DateTime IndexedAt { set; get; }
        public string[] Labels { set; get; }
        public Viewer Viewer { set; get; }
    }
}
