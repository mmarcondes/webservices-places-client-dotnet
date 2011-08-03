using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Converters
{
    public interface IPlacesConverter
    {
        IEnumerable<Place> ToEntity(Resources.Places placesResource);
    }
}