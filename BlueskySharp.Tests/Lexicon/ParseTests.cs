using BlueskySharp.Lexicon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueskySharp.Tests.Lexicon
{
    [TestClass]
    public class ParseTests
    {
        public const string TestPayload =
            """
            {
                "lexicon": 1,
                "id": "com.example.getProfile",
                "defs": {
                    "main": {
                        "type": "query",
                        "parameters": {
                            "type": "params",
                            "required": ["handle"],
                            "properties": { "handle": { "type": "string" } }
                        },
                        "output": {
                            "encoding": "application/json"
                        }
                    }
                }
            }
            """;

        [TestMethod]
        public void TestParse()
        {
            var document = LexiconParser.ParseText(TestPayload);

            Assert.IsNotNull(document);
            Assert.AreEqual(document.Lexicon, 1);
        }
    }
}
