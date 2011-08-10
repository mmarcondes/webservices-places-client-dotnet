using System;
using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;
using System.Linq;

namespace Maplink.Webservices.Places.Client.Converters
{
    public class PlacesConverter : IPlacesConverter
    {
        public PlaceSearchResult ToEntity(Resources.Places placesResource)
        {
            return placesResource != null
                ? ConvertToEntity(placesResource)
                : new PlaceSearchResult();
        }

        private static PlaceSearchResult ConvertToEntity(Resources.Places placesResource)
        {
            var nextAtomLink = placesResource.Links.FirstOrDefault(it => it.Rel == "next");
            var previousAtomLink = placesResource.Links.FirstOrDefault(it => it.Rel == "previous");
            return new PlaceSearchResult
                       {
                           Places = ToEntity(placesResource.Retrieved),
                           TotalFound = placesResource.TotalFound,
                           NextPageUri = nextAtomLink != null ? nextAtomLink.Href : String.Empty,
                           PreviousPageUri = previousAtomLink != null ? previousAtomLink.Href : String.Empty
                       };
        }

        private static IEnumerable<Place> ToEntity(IEnumerable<Resources.Place> resources)
        {
            return resources
                .Select(
                    place =>
                    new Place
                        {
                            Id = place.Id,
                            Address = place.Address,
                            Category = place.Category,
                            City = place.City,
                            Country = place.Country,
                            Description = place.Description,
                            District = place.District,
                            Latitude = place.Latitude,
                            Longitude = place.Longitude,
                            Name = place.Name,
                            PrimaryPhone = place.PrimaryPhone,
                            SecondaryPhone = place.SecondaryPhone,
                            State = place.State,
                            SubCategory = place.SubCategory,
                            ZipCode = place.ZipCode,
                            DistanceInKilometers = place.Distance
                        });
        }
    }
}
