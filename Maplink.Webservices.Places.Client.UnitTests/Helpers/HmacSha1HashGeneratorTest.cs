using Maplink.Webservices.Places.Client.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Helpers
{
    [TestClass]
    public class HmacSha1HashGeneratorTest
    {
        [TestMethod]
        public void ShouldGenerateHash()
        {
            const string content = "my\ncontent";
            const string key = "my-key";

            new HmacSha1HashGenerator().For(content, key)
                .Should().Be.EqualTo("34cbdcfcaf9495e1c29a5602aa4ab187370f1799");
        }
    }
}
