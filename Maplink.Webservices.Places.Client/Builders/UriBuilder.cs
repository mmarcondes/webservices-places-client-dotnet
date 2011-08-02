using System.Globalization;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class UriBuilder : IUriBuilder
    {
        private readonly IConfigurationWrapper _configurationWrapper;
        private const string BaseUriKey = "Maplink.Webservices.Places.BaseUri";

        public UriBuilder(IConfigurationWrapper configurationWrapper)
        {
            _configurationWrapper = configurationWrapper;
        }

        public string ForRadiusSearch(RadiusSearchRequest request)
        {
            var culture = CultureInfo.GetCultureInfo("en-us");

            return string.Format(
                "{0}?radius={1}&latitude={2}&longitude={3}",
                _configurationWrapper.ValueFor(BaseUriKey),
                request.Radius,
                request.Latitude.ToString(culture),
                request.Longitude.ToString(culture));
        }
    }
}