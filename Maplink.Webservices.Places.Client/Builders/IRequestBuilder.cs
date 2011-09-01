using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IRequestBuilder
    {
        IRequestBuilder WithUriPath(string uriPath);
        IRequestBuilder WithUriPathAndQuery(string uriPathAndQuery);
        IRequestBuilder WithStartIndex(int startIndex);
        IRequestBuilder WithLicenseInfo(string login, string key);
        IRequestBuilder WithLicenseInfo(LicenseInfo licenseInfo);
        IRequestBuilder WithArgument(string key, string value);
        Request Build();
    }
}