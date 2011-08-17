using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;
using System.Linq;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class SearchRequestBuilder : ISearchRequestBuilder
    {
        private readonly SearchRequest _searchRequest;

        public SearchRequestBuilder()
        {
            _searchRequest = new SearchRequest {Arguments = new Dictionary<string, string>()};
        }

        public ISearchRequestBuilder WithUriPath(string uriPath)
        {
            _searchRequest.UriPath = uriPath;

            return this;
        }

        public ISearchRequestBuilder WithStartIndex(int startIndex)
        {
            _searchRequest.StartsAtIndex = startIndex;

            return this;
        }
 
        public ISearchRequestBuilder WithLicenseInfo(string login, string key)
        {
            _searchRequest.LicenseLogin = login;
            _searchRequest.LicenseKey = key;

            return this;
        }

        public ISearchRequestBuilder WithArgument(string key, string value)
        {
            _searchRequest.Arguments = _searchRequest
                .Arguments
                .Concat(new Dictionary<string, string> {{key, value}});

            return this;
        }

        public SearchRequest Build()
        {
            return _searchRequest; 
        }
        
    }
}
