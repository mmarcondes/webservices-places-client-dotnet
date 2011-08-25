using System;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class 
        UriBuilder : IUriBuilder
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
                "{0}{1}?{2}{3}", 
                _configurationWrapper.ValueFor(BaseUriKey), 
                request.UriPath, 
                uriQuery,
                CreateQueryStringForStartIndex(request));
        }

        private static string CreateQueryStringForStartIndex(Request request)
        {
            return request.StartsAtIndex > 0
                ? String.Format("&start={0}", request.StartsAtIndex)
                : String.Empty ;
        }
    }
}