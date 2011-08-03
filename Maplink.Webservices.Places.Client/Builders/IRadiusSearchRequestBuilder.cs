using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IRadiusSearchRequestBuilder
    {
        RadiusSearchRequestBuilder WithRadius(int radius);
        RadiusSearchRequestBuilder WithLatitude(double latitude);
        RadiusSearchRequestBuilder WithLongitude(double longitude);
        RadiusSearchRequestBuilder WithKey(string key);
        RadiusSearchRequestBuilder WithLogin(string login);
        RadiusSearchRequest Build();
    }
}