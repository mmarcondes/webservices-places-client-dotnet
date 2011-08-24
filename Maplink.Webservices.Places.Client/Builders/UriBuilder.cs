using System;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class UriBuilder : IUriBuilder
    {
        private readonly IConfigurationWrapper _configurationWrapper;
        private readonly IUriQueryBuilder _uriQueryBuilder;
        private const string BaseUriKey = "Maplink.Webservices.Places.BaseUri";

        public UriBuilder(IConfigurationWrapper configurationWrapper, IUriQueryBuilder uriQueryBuilder)
        {
            _configurationWrapper = configurationWrapper;
            _uriQueryBuilder = uriQueryBuilder;
        }

        public string For(Request request)
        {
            var uriQuery = _uriQueryBuilder.Build(request.Arguments);

            return String.Format(
                "{0}/{1}?{2}", 
                _configurationWrapper.ValueFor(BaseUriKey), 
                request.UriPath, 
                uriQuery);
        }

        public string ForPagination(string uri)
        {
            return String.Format(
                "{0}{1}",
                _configurationWrapper.ValueFor(BaseUriKey),
                uri);
        }
    }
}