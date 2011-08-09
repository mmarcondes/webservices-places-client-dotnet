using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Factories
{
    [TestClass]
    public class PlaceResultFactoryTest
    {
        [TestMethod]
        public void ShouldCreateAPlaceResult()
        {
            var places = new List<Place>();

            var result = new PlaceResultFactory().Create(places, 10, 2);

            result.Places.Should().Be.SameInstanceAs(places);
            result.TotalFound.Should().Be.EqualTo(10);
            result.StartedAtIndex.Should().Be.EqualTo(2);
        }
    }
}
