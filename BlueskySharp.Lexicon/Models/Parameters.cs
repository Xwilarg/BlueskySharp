namespace BlueskySharp.Lexicon.Models
{
    public record Parameters : Definition
    {
        public List<string> Required { get; set; }
        public Dictionary<string, Definition> Properties { get; set; }
    }
}
