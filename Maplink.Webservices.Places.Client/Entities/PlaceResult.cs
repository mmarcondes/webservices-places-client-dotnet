using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Entities
{
    public class PlaceResult
    {
        public PlaceResult()
        {
            Places = new List<Place>();
        }

        public IEnumerable<Place> Places { get; set; }
        public int TotalFound { get; set; }
        public int StartedAtIndex { get; set; }
    }
}