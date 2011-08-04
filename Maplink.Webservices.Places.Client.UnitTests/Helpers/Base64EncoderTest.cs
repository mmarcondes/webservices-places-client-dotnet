using Maplink.Webservices.Places.Client.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Helpers
{
    [TestClass]
    public class Base64EncoderTest
    {
        [TestMethod]
        public void ShouldEncodeToBase64()
        {
            new Base64Encoder().Encode("my-string")
                .Should().Be.EqualTo("bXktc3RyaW5n");
        }
    }
}
