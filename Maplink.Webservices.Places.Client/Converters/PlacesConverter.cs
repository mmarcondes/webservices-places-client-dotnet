using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;
using System.Linq;

namespace Maplink.Webservices.Places.Client.Converters
{
    public class PlacesConverter : IPlacesConverter
    {
        public IEnumerable<Place> ToEntity(Resources.Places placesResource)
        {
            return placesResource.All
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
                            ZipCode = place.ZipCode
                        });
        }
    }
}
