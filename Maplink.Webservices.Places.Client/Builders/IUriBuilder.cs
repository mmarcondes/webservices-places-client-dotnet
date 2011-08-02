using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IUriBuilder
    {
        string ForRadiusSearch(RadiusSearchRequest request);
    }
}