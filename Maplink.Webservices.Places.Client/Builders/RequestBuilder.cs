using System;
using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;
using System.Linq;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class RequestBuilder : IRequestBuilder
    {
        private readonly Request _request;

        public RequestBuilder()
        {
            _request = new Request {Arguments = new Dictionary<string, string>()};
        }
        
        public IRequestBuilder WithUriPathAndQuery(string uriPathAndQuery)
        {
            var splittedUriAndPath = uriPathAndQuery.Split('?');

            if (ExistsQueryString(splittedUriAndPath))
            {
                var queryString = splittedUriAndPath.LastOrDefault();

                SetArgumentsFromQueryString(queryString);
            }

            return WithUriPath(splittedUriAndPath.FirstOrDefault());
        }

        public IRequestBuilder WithUriPath(string uriPath)
        {
            _request.UriPath = uriPath;

            return this;
        }

        public IRequestBuilder WithStartIndex(int startIndex)
        {
            _request.StartsAtIndex = startIndex;

            return this;
        }
 
        public IRequestBuilder WithLicenseInfo(string login, string key)
        {
            _request.LicenseLogin = login;
            _request.LicenseKey = key;

            return this;
        }

        public IRequestBuilder WithArgument(string key, string value)
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

        private static bool ExistsQueryString(IEnumerable<string> splittedUriAndPath)
        {
            return splittedUriAndPath.Count() > 1;
        }

        private void SetArgumentsFromQueryString(string queryString)
        {
            if (queryString == null) return;

            foreach (var argumentWithKeyAndValue in queryString.Split('&').Select(argument => argument.Split('=')))
            {
                var argumentKey = argumentWithKeyAndValue.FirstOrDefault();
                var argumentValue = argumentWithKeyAndValue.LastOrDefault();

                if(argumentKey.Equals("start"))
                {
                    WithStartIndex(ExtractStartIndexFromArgumentValue(argumentValue));
                    break;
                }

                WithArgument(argumentKey, argumentValue);
            }
        }

        private static int ExtractStartIndexFromArgumentValue(string argumentValue)
        {
            int startIndex;

            Int32.TryParse(argumentValue, out startIndex);
            return startIndex;
        }
    }
}
