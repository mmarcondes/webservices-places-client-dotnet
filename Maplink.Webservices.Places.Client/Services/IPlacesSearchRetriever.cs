using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Services
{
    public interface IPlacesSearchRetriever
    {
        Resources.Places RetrieveFrom(Request request);
        Resources.Places RetrieveFrom(CustomRequest customRequest);
    }
}