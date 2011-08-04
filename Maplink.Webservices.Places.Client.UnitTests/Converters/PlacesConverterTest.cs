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
        private static Webservices.Places.Client.Resources.Places _placesResources;
        private IPlacesConverter _converter;

        [TestInitialize]
        public void SetUp()
        {
            CreateResources();
            _converter = new PlacesConverter();
        }

        [TestMethod]
        public void ShouldConvertToEntities()
        {
            var places = _converter.ToEntity(_placesResources).ToList();

            places.Should().Have.Count.EqualTo(2);
            AssertThis(_firstPlaceResource, places.First());
            AssertThis(_secondPlaceResource, places.Last()); 
        }

        [TestMethod]
        public void ShouldBeAnEmptyListIfTryToConvertToEntityAnNullResource()
        {
            _converter.ToEntity(null).Should().Be.Empty();
        }

        private static void AssertThis(Place resource, Webservices.Places.Client.Entities.Place entity)
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

            _placesResources = new Webservices.Places.Client.Resources.Places
                                      {
                                          All = new List<Place>
                                                    {
                                                        _firstPlaceResource,
                                                        _secondPlaceResource
                                                    }
                                      };
        }
    }
}
