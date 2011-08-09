using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Entities
{
    public class PlaceResult
    {
        public IEnumerable<Place> Places { get; set; }
        public int TotalFound { get; set; }
        public int StartedAtIndex { get; set; }
    }
}