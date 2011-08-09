using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Factories
{
    public class PlaceResultFactory : IPlaceResultFactory
    {
        public PlaceResult Create(
            IEnumerable<Place> places,
            int totalFound,
            int startedAtIndex)
        {
            return new PlaceResult
                       {
                           Places = places,
                           TotalFound = totalFound,
                           StartedAtIndex = startedAtIndex
                       };
        }
    }
}
