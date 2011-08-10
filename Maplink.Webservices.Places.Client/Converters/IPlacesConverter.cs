using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Converters
{
    public interface IPlacesConverter
    {
        PlaceSearchResult ToEntity(Resources.Places placesResource);
    }
}