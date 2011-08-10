using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Exceptions;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Services
{
    public class PlacesSearchRetriever : IPlacesSearchRetriever
    {
        private readonly IRequestBuilder _requestBuilder;
        private readonly IHttpClient _httpClient;
        private readonly IXmlSerializerWrapper _serializerWrapper;

        public PlacesSearchRetriever(
            IRequestBuilder requestBuilder,
            IHttpClient httpClient,
            IXmlSerializerWrapper serializerWrapper)
        {
            _requestBuilder = requestBuilder;
            _httpClient = httpClient;
            _serializerWrapper = serializerWrapper;
        }

        public Resources.Places RetrieveFrom(RadiusSearchRequest radiusSearchRequest)
        {
            var request = _requestBuilder.ForRadiusSearch(radiusSearchRequest);

            return RetrieveFrom(request);
        }

        public Resources.Places RetrieveFrom(PlaceSearchPaginationRequest placeSearchPaginationRequest)
        {
            var request = _requestBuilder.ForRadiusSearch(placeSearchPaginationRequest);

            return RetrieveFrom(request);
        }

        private Resources.Places RetrieveFrom(Request request)
        {
            var response = _httpClient.Get(request);

            if (IsAnInvalidResponse(response))
            {
                throw new PlaceClientRequestException(response.StatusCode);
            }

            return HasAnyResourceBeenFound(response)
                       ? _serializerWrapper.Deserialize<Resources.Places>(response.Body)
                       : new Resources.Places();
        }

        private static bool HasAnyResourceBeenFound(Response response)
        {
            return response.StatusCode != 404;
        }

        private static bool IsAnInvalidResponse(Response response)
        {
            return !response.Success && response.StatusCode != 404;
        }
    }
}