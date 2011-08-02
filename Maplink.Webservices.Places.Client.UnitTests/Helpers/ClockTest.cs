using System;
using Maplink.Webservices.Places.Client.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Client.Places.UnitTests.Helpers
{
    [TestClass]
    public class ClockTest
    {
        [TestMethod]
        public void ShouldRetrieveUtcNow()
        {
            new Clock().UtcNow().Date.Should().Be.EqualTo(DateTime.UtcNow.Date);
        }
    }
}
