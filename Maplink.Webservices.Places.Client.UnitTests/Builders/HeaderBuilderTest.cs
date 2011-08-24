using System;
using Maplink.Webservices.Places.Client.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Builders
{
    [TestClass]
    public class HeaderBuilderTest
    {
        [TestMethod]
        public void ShouldCreateHeaderForXMaplinkDate()
        {
            var header = new HttpHeaderBuilder().ForXMaplinkDate(new DateTime(2011, 08, 01));
            header.Key.Should().Be.EqualTo("X-Maplink-Date");
            header.Value.Should().Be.EqualTo("Mon, 01 Aug 2011 00:00:00 GMT");
        }

        [TestMethod]
        public void ShouldCreateHeaderForAuthorization()
        {
            var header = new HttpHeaderBuilder().ForAuthorization("anystring");
            header.Key.Should().Be.EqualTo("Authorization");
            header.Value.Should().Be.EqualTo("MAPLINKWS anystring");
        }
    }
}
