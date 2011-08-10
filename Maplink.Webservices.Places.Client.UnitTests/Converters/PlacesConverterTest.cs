using System.Collections.Generic;
using System.Linq;
using Maplink.Webservices.Places.Client.Converters;
using Maplink.Webservices.Places.Client.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Converters
{
    [TestClass]
    public class PlacesConverterTest
    {
        private static Place _firstPlaceResource;
        private static Place _secondPlaceResource;
        private static Client.Resources.Places _placesResources;
        private IPlacesConverter _converter;

        [TestInitialize]
        public void SetUp()
        {
            CreateResources();
            _converter = new PlacesConverter();
        }

        [TestMethod]
        public void ShouldConvertToEntity()
        {
            GivenAllLinksWereRetrieved();
            
            var placeSearchResult = _converter.ToEntity(_placesResources);

            placeSearchResult.Places.Should().Have.Count.EqualTo(2);
            AssertThis(_firstPlaceResource, placeSearchResult.Places.First());
            AssertThis(_secondPlaceResource, placeSearchResult.Places.Last());
            placeSearchResult.TotalFound.Should().Be.EqualTo(20);
            placeSearchResult.NextPageUri.Should().Be.EqualTo("next-uri");
            placeSearchResult.PreviousPageUri.Should().Be.EqualTo("previous-uri");
        }

        [TestMethod]
        public void ShouldNotContainPreviousPageUri()
        {
            GivenThePreviousLinkWasNotRetrieved();

            var placeSearchResult = _converter.ToEntity(_placesResources);

            placeSearchResult.PreviousPageUri.Should().Be.Empty();
        }

        [TestMethod]
        public void ShouldNotContainNextPageUri()
        {
            GivenTheNextLinkWasNotRetrieved();

            var placeSearchResult = _converter.ToEntity(_placesResources);

            placeSearchResult.NextPageUri.Should().Be.Empty();
        }

        [TestMethod]
        public void ShouldBeAnEmptyListIfTryToConvertToEntityAnNullResource()
        {
            _converter.ToEntity(null).Places.Should().Be.Empty();
        }

        private static void AssertThis(Place resource, Entities.Place entity)
        {
            entity.Address.Should().Be.EqualTo(resource.Address);
            entity.Category.Should().Be.EqualTo(resource.Category);
            entity.City.Should().Be.EqualTo(resource.City);
            entity.Country.Should().Be.EqualTo(resource.Country);
            entity.Description.Should().Be.EqualTo(resource.Description);
            entity.District.Should().Be.EqualTo(resource.District);
            entity.Id.Should().Be.EqualTo(resource.Id);
            entity.Latitude.Should().Be.EqualTo(resource.Latitude);
            entity.Longitude.Should().Be.EqualTo(resource.Longitude);
            entity.Name.Should().Be.EqualTo(resource.Name);
            entity.PrimaryPhone.Should().Be.EqualTo(resource.PrimaryPhone);
            entity.SecondaryPhone.Should().Be.EqualTo(resource.SecondaryPhone);
            entity.State.Should().Be.EqualTo(resource.State);
            entity.SubCategory.Should().Be.EqualTo(resource.SubCategory);
            entity.ZipCode.Should().Be.EqualTo(resource.ZipCode);
            entity.DistanceInKilometers.Should().Be.EqualTo(resource.Distance);
        }

        private static void CreateResources()
        {
            _firstPlaceResource = new Place
                                         {
                                             Id = "42",
                                             Address = "address",
                                             Category = "category",
                                             City = "city",
                                             Country = "country",
                                             Description = "description",
                                             Distance = 124.235,
                                             District = "district",
                                             Latitude = -42.325,
                                             Longitude = -42.467,
                                             Name = "name",
                                             PrimaryPhone = "primary-phone",
                                             SecondaryPhone = "secondary-phone",
                                             State = "state",
                                             SubCategory = "subcategory",
                                             ZipCode = "zipcode"
                                         };

            _secondPlaceResource = new Place
                                          {
                                              Id = "43"
                                          };

            _placesResources = new Client.Resources.Places
                                      {
                                          Retrieved = new List<Place>
                                                    {
                                                        _firstPlaceResource,
                                                        _secondPlaceResource
                                                    },
                                          TotalFound = 20
                                      };
        }

        private void GivenAllLinksWereRetrieved()
        {
            _placesResources.Links = new List<AtomLink>
                                         {
                                             new AtomLink {Rel = "next", Href = "next-uri"},
                                             new AtomLink {Rel = "previous", Href = "previous-uri"},
                                         };
        }

        private void GivenThePreviousLinkWasNotRetrieved()
        {
            _placesResources.Links = new List<AtomLink>
                                         {
                                             new AtomLink {Rel = "next", Href = "next-uri"}
                                         };
        }

        private void GivenTheNextLinkWasNotRetrieved()
        {
            _placesResources.Links = new List<AtomLink>
                                         {
                                             new AtomLink {Rel = "previous", Href = "previous-uri"}
                                         };
        }
    }
}
