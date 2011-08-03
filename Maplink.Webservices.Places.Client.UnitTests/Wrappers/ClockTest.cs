using System;
using Maplink.Webservices.Places.Client.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Client.Places.UnitTests.Wrappers
{
    [TestClass]
    public class ClockTest
    {
        [TestMethod]
        public void ShouldRetrieveUtcNow()
        {
            new Clock().UtcHourNow().Date.Should().Be.EqualTo(DateTime.UtcNow.Date);
        }
    }
}
