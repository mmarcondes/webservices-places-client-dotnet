using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client
{
    public interface IPlaceSearcher
    {
        IEnumerable<Place> ByRadius(RadiusSearchRequest radiusSearchRequest);
    }
}