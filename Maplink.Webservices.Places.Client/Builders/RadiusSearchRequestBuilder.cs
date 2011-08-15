using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class RadiusSearchRequestBuilder : IRadiusSearchRequestBuilder
    {
        private readonly RadiusSearchRequest _instance;

        public RadiusSearchRequestBuilder()
        {
            _instance = new RadiusSearchRequest();
        }

        public RadiusSearchRequestBuilder WithRadius(double radius)
        {
            _instance.Radius = radius;

            return this;
        }

        public RadiusSearchRequestBuilder WithLatitude(double latitude)
        {
            _instance.Latitude = latitude;

            return this;
        }

        public RadiusSearchRequestBuilder WithLongitude(double longitude)
        {
            _instance.Longitude = longitude;

            return this;
        }

        public RadiusSearchRequestBuilder WithKey(string key)
        {
            _instance.Key = key;

            return this;
        }

        public RadiusSearchRequestBuilder WithLogin(string login)
        {
            _instance.Login = login;

            return this;
        }

        public RadiusSearchRequestBuilder StartingAtIndex(int index)
        {
            _instance.StartsAtIndex = index;

            return this;
        }

        public RadiusSearchRequest Build()
        {
            return _instance;
        }
    }
}
