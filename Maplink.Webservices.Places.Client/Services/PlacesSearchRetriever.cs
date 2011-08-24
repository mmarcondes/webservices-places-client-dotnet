using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Exceptions;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Services
{
    public class PlacesSearchRetriever : IPlacesSearchRetriever
    {
        private readonly IHttpRequestBuilder _httpRequestBuilder;
        private readonly IHttpClient _httpClient;
        private readonly IXmlSerializerWrapper _serializerWrapper;

        public PlacesSearchRetriever(
            IHttpRequestBuilder httpRequestBuilder,
            IHttpClient httpClient,
            IXmlSerializerWrapper serializerWrapper)
        {
            _httpRequestBuilder = httpRequestBuilder;
            _httpClient = httpClient;
            _serializerWrapper = serializerWrapper;
        }
        
        public Resources.Places RetrieveFrom(SearchRequest searchRequest)
        {
            var request = _httpRequestBuilder.For(searchRequest);

            return RetrieveFrom(request);
        }

        public Resources.Places RetrieveFrom(CustomRequest customRequest)
        {
            var request = _httpRequestBuilder.ForCustomRequest(customRequest);

            return RetrieveFrom(request);
        }

        private Resources.Places RetrieveFrom(HttpRequest httpRequest)
        {
            var response = _httpClient.Get(httpRequest);

            if (IsAnInvalidResponse(response))
            {
                throw new PlaceClientRequestException(response.StatusCode);
            }

            return HasAnyResourceBeenFound(response)
                       ? _serializerWrapper.Deserialize<Resources.Places>(response.Body)
                       : new Resources.Places();
        }

        private static bool HasAnyResourceBeenFound(HttpResponse httpResponse)
        {
            return httpResponse.StatusCode != 404;
        }

        private static bool IsAnInvalidResponse(HttpResponse httpResponse)
        {
            return !httpResponse.Success && httpResponse.StatusCode != 404;
        }
    }
}