using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Entities
{
    public class PlaceSearchResult
    {
        public PlaceSearchResult()
        {
            Places = new List<Place>();
        }

        public IEnumerable<Place> Places { get; set; }
        public string NextPageUri { get; set; }
        public string PreviousPageUri { get; set; }
        public int TotalFound { get; set; }
    }
}