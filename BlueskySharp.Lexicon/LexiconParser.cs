using BlueskySharp.Lexicon.Models;
using System.Text;
using System.Text.Json;

namespace BlueskySharp.Lexicon
{
    public class LexiconParser
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static Document ParseText(string content)
        {
            using var stream = new MemoryStream(
                    Encoding.UTF8.GetBytes(content)
                );

            return ParseFile(stream);
        }

        public static Document ParseFile(string file)
        {
            using var content = File.OpenRead(file);

            return ParseFile(content);
        }

        public static Document ParseFile(Stream stream)
        {
            return JsonSerializer.Deserialize<Document>(stream, _options);
        }

        public static List<Document> ParseDirectory(string path)
        {
            throw new NotImplementedException();
        }
    }
}
