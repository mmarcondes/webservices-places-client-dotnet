using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Exceptions;
using Maplink.Webservices.Places.Client.Resources;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Services
{
    public class ResourceRetriever : IResourceRetriever
    {
        private readonly IHttpRequestBuilder _httpRequestBuilder;
        private readonly IHttpClient _httpClient;
        private readonly IXmlSerializerWrapper _serializerWrapper;

        public ResourceRetriever(
            IHttpRequestBuilder httpRequestBuilder,
            IHttpClient httpClient,
            IXmlSerializerWrapper serializerWrapper)
        {
            _httpRequestBuilder = httpRequestBuilder;
            _httpClient = httpClient;
            _serializerWrapper = serializerWrapper;
        }

        public TResource From<TResource>(Request request) where TResource : Resource, new()
        {
            var httpRequest = _httpRequestBuilder.For(request);

            return RetrieveFrom<TResource>(httpRequest);
        }

        private TResource RetrieveFrom<TResource>(HttpRequest httpRequest) where TResource : Resource, new()
        {
            var response = _httpClient.Get(httpRequest);

            if (IsAnInvalidResponse(response))
            {
                throw new PlaceClientRequestException(response.StatusCode);
            }

            return HasAnyResourceBeenFound(response)
                       ? _serializerWrapper.Deserialize<TResource>(response.Body)
                       : new TResource();
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