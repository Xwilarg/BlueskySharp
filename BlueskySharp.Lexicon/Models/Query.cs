namespace BlueskySharp.Lexicon.Models
{
    public record Query : Definition
    {
        public Parameters Parameters { get; set; }
        public Body Input { get; set; }
        public Body Output { get; set; }
    }
}
