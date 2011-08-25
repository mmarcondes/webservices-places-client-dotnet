using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Converters;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Helpers;
using Maplink.Webservices.Places.Client.Resources;
using Maplink.Webservices.Places.Client.Services;
using Maplink.Webservices.Places.Client.Wrappers;
using Category = Maplink.Webservices.Places.Client.Entities.Category;

namespace Maplink.Webservices.Places.Client
{
    public class CategorySearcher : ICategorySearcher
    {
        private readonly IRequestBuilder _requestBuilder;
        private readonly IResourceRetriever _resourceRetriever;
        private readonly ICategoryResourceConverter _categoryResourceConverter;

        public CategorySearcher(
            IRequestBuilder requestBuilder, 
            IResourceRetriever resourceRetriever, 
            ICategoryResourceConverter categoryResourceConverter)
        {
            _requestBuilder = requestBuilder;
            _resourceRetriever = resourceRetriever;
            _categoryResourceConverter = categoryResourceConverter;
        }

        public CategorySearcher() : this(
            new RequestBuilder(),
            new ResourceRetriever(
                    new HttpRequestBuilder(
                        new UriBuilder(new ConfigurationWrapper(), new UriQueryBuilder()),
                        new Clock(),
                        new SignatureBuilder(new HmacSha1HashGenerator()),
                        new AllHttpHeadersBuilder(new HttpHeaderBuilder(), new AuthorizationBuilder(new Base64Encoder()))),
                    new HttpClient(),
                    new XmlSerializerWrapper()),
            new CategoryResourceConverter())
        {
            
        }

        public IEnumerable<Category> All(LicenseInfo licenseInfo)
        {
            var requestBuild =
                _requestBuilder
                    .WithLicenseInfo(licenseInfo.Login, licenseInfo.Key)
                    .WithUriPath("/categories")
                    .WithStartIndex(0)
                    .Build();

            var resourceCategories = _resourceRetriever.From<Categories>(requestBuild);

            return _categoryResourceConverter.ToEntity(resourceCategories.All);
        }
    }
}