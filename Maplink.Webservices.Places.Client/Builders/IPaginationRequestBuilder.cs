using Maplink.Webservices.Places.Client.Arguments;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IPaginationRequestBuilder
    {
        IPaginationRequestBuilder ForLicense(string login, string key);
        IPaginationRequestBuilder WithUriPathAndQuery(string uriPathAndQuery);
        PaginationRequest Build();
    }
}