using System;
using Maplink.Webservices.Places.Client.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Wrappers
{
    [TestClass]
    public class ClockTest
    {
        [TestMethod]
        public void ShouldRetrieveUtcHourNowForCachingPurpose()
        {
            var now = DateTime.Now;

            var expectedDate = 
                new DateTime(now.Year, now.Month, now.Day, now.Hour, 1, 1, 1)
                .ToUniversalTime();

            new Clock().UtcHourNow().Should().Be.EqualTo(expectedDate);
        }
    }
}
