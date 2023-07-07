using System.Text.Json.Serialization;

namespace BlueskySharp.Response
{
    public class ImageMetadata
    {
        [JsonPropertyName("$type")]
        public string Type { set; get; }
        public object Ref { set; get; } // TODO: parse properly
        public string MimeType { set; get; }
        public int Size { set; get; }
    }

    public class ImageInfo
    {
        public ImageMetadata[] Image;
        public string Alt;
    }

    public class Image
    {
        public string Thumb { set; get; }
        public string Fullsize { set; get; }
        public string Alt { set; get; }
    }

    public class Embed
    {
        [JsonPropertyName("$type")]
        public string Type { set; get; } // TODO: Replace by enum
        public Image[] Images { set; get; }
    }

    public record Record
    {
        public string Text { set; get; }
        [JsonPropertyName("$type")]
        public string Type { set; get; }
        public Embed Embed { set; get; }
        public string[] Langs { set; get; }
        public DateTime CreatedAt { set; get; }
    }

    public record Post
    {
        public string Uri { set; get; }
        public string Cid { set; get; }
        public ProfileShort Author { set; get; }
        public Record Record { set; get; }
        public Embed Embed { set; get; }
        public int ReplyCount { set; get; }
        public int RepostCount { set; get; }
        public int LikeCount { set; get; }
        public DateTime IndexedAt { set; get; }
        public Viewer Viewer { set; get; }
        public string[] Labels { set; get; }
    }

    public record FeedViewPost
    {
        public Post Post { get; set; }
    }

    public record Timeline
    {
        public string Cursor { set; get; }
        public FeedViewPost[] Feed { set; get; }
    }
}
