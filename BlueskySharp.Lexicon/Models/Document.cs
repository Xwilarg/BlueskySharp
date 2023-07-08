namespace BlueskySharp.Lexicon.Models
{
    public record Document
    {
        public int Lexicon { get; set; }
        public string Id { get; set; }
        public int Revision { get; set; }
        public string Description { get; set; }

        public IReadOnlyDictionary<string, Definition> Defs { get; set; }
    }
}
