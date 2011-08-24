using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface ICustomRequestBuilder
    {
        ICustomRequestBuilder WithLicenseInfo(string licenseLogin, string licenseKey);
        ICustomRequestBuilder WithUriPathAndQuery(string uri);
        CustomRequest Build();
    }
}