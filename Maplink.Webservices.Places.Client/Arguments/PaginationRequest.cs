using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Arguments
{
    public class PaginationRequest
    {
        public LicenseInfo LicenseInfo { get; set; }
        public string UriPathAndQuery { get; set; }
    }
}