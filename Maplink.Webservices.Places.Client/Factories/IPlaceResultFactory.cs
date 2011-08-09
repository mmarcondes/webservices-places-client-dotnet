using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Factories
{
    public interface IPlaceResultFactory
    {
        PlaceResult Create(
            IEnumerable<Place> places,
            int totalFound,
            int startedAtIndex);
    }
}