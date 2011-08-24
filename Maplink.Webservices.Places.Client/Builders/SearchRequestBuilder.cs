using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;
using System.Linq;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class SearchRequestBuilder : ISearchRequestBuilder
    {
        private readonly Request _request;

        public SearchRequestBuilder()
        {
            _request = new Request {Arguments = new Dictionary<string, string>()};
        }

        public ISearchRequestBuilder WithUriPath(string uriPath)
        {
            _request.UriPath = uriPath;

            return this;
        }

        public ISearchRequestBuilder WithStartIndex(int startIndex)
        {
            _request.StartsAtIndex = startIndex;

            return this;
        }
 
        public ISearchRequestBuilder WithLicenseInfo(string login, string key)
        {
            _request.LicenseLogin = login;
            _request.LicenseKey = key;

            return this;
        }

        public ISearchRequestBuilder WithArgument(string key, string value)
        {
            _request.Arguments = _request
                .Arguments
                .Concat(new Dictionary<string, string> {{key, value}});

            return this;
        }

        public Request Build()
        {
            return _request; 
        }
        
    }
}
