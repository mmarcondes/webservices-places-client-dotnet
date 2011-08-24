using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface ISearchRequestBuilder
    {
        ISearchRequestBuilder WithUriPath(string uriPath);
        ISearchRequestBuilder WithStartIndex(int startIndex);
        ISearchRequestBuilder WithLicenseInfo(string login, string key);
        ISearchRequestBuilder WithArgument(string key, string value);
        Request Build();
    }
}