using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Converters;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Resources;
using Maplink.Webservices.Places.Client.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;
using Category = Maplink.Webservices.Places.Client.Entities.Category;

namespace Maplink.Webservices.Places.Client.UnitTests
{
    [TestClass]
    public class CategorySearcherTest
    {
        private ICategorySearcher _searcher;
        private Mock<IRequestBuilder> _mockedRequestBuilder;
        private Mock<IResourceRetriever> _mockedResourceRetriever;
        private Mock<ICategoryResourceConverter> _mockedCategoryConverter;
        private IEnumerable<Category> _convertedCategories;
        private LicenseInfo _licenseInfo;
        private Request _requestBuilt;
        private Categories _resourceRetrieved;

        [TestInitialize]
        public void SetUp()
        {
            _licenseInfo = new LicenseInfo();

            _mockedRequestBuilder = new Mock<IRequestBuilder>();
            _mockedResourceRetriever = new Mock<IResourceRetriever>();
            _mockedCategoryConverter = new Mock<ICategoryResourceConverter>();

            _searcher = new CategorySearcher(
                _mockedRequestBuilder.Object,
                _mockedResourceRetriever.Object,
                _mockedCategoryConverter.Object);
        }

        [TestMethod]
        public void ShouldListAllCategoriesAvailable()
        {
            GivenTheRequestCanBeBuilt()
                .AndTheResourceCanBeRetrieved()
                .AndTheResourceCanBeConverted()
                .WhenListingAllCategories()
                .Should().Be.SameInstanceAs(_convertedCategories);
        }

        [TestMethod]
        public void ShouldBuildRequestWithUriPathWhenListingCategoriesAvailable()
        {
            GivenTheRequestCanBeBuilt()
                .AndTheResourceCanBeRetrieved()
                .AndTheResourceCanBeConverted()
                .WhenListingAllCategories();

            _mockedRequestBuilder.Verify(it => it.WithUriPath("/categories"), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildRequestWithStartIndexWhenListingCategoriesAvailable()
        {
            GivenTheRequestCanBeBuilt()
                .AndTheResourceCanBeRetrieved()
                .AndTheResourceCanBeConverted()
                .WhenListingAllCategories();

            _mockedRequestBuilder.Verify(it => it.WithStartIndex(0), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildRequestWhenListingCategoriesAvailable()
        {
            GivenTheRequestCanBeBuilt()
                .AndTheResourceCanBeRetrieved()
                .AndTheResourceCanBeConverted()
                .WhenListingAllCategories();

            _mockedRequestBuilder.Verify(it => it.Build(), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrieveCategoriesWhenListingCategoriesAvailable()
        {
            GivenTheRequestCanBeBuilt()
                .AndTheResourceCanBeRetrieved()
                .AndTheResourceCanBeConverted()
                .WhenListingAllCategories();

            _mockedResourceRetriever.Verify(it => it.From<Categories>(_requestBuilt), Times.Once());
        }

        [TestMethod]
        public void ShouldConvertCategoriesToEntitiesWhenListingCategoriesAvailable()
        {
            GivenTheRequestCanBeBuilt()
                .AndTheResourceCanBeRetrieved()
                .AndTheResourceCanBeConverted()
                .WhenListingAllCategories();

            _mockedCategoryConverter.Verify(it => it.ToEntity(_resourceRetrieved.All), Times.Once());
        }

        private CategorySearcherTest GivenTheRequestCanBeBuilt()
        {
            _requestBuilt = new Request();
            
            _mockedRequestBuilder
                .Setup(it => it.WithUriPath(It.IsAny<string>()))
                .Returns(_mockedRequestBuilder.Object);
            _mockedRequestBuilder
                .Setup(it => it.WithStartIndex(It.IsAny<int>()))
                .Returns(_mockedRequestBuilder.Object);
            _mockedRequestBuilder
                .Setup(it => it.Build())
                .Returns(_requestBuilt);

            return this;
        }

        private CategorySearcherTest AndTheResourceCanBeRetrieved()
        {
            _resourceRetrieved = new Categories
                                     {
                                         All = new List<Client.Resources.Category>()
                                     };
            
            _mockedResourceRetriever
                .Setup(it => it.From<Categories>(It.IsAny<Request>()))
                .Returns(_resourceRetrieved);

            return this;
        }

        private CategorySearcherTest AndTheResourceCanBeConverted()
        {
            _convertedCategories = new List<Category>();
            _mockedCategoryConverter
                .Setup(it => it.ToEntity(It.IsAny<IEnumerable<Client.Resources.Category>>()))
                .Returns(_convertedCategories);

            return this;
        }

        private IEnumerable<Category> WhenListingAllCategories()
        {
            return _searcher.All(_licenseInfo);
        }
    }
}
