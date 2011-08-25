using System.Collections.Generic;
using System.Linq;
using Maplink.Webservices.Places.Client.Converters;
using Maplink.Webservices.Places.Client.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Converters
{
    [TestClass]
    public class CategoryResourceConverterTest
    {
        [TestMethod]
        public void ShouldConvertCategoriesResourceToEntity()
        {
            var firstCategoryResource = new Category{ Id = 42, Name = "first" };
            var secondCategoryResource = new Category { Id = 43, Name = "second" };

            var categoriesResource = 
                new List<Category>
                    {
                        firstCategoryResource,
                        secondCategoryResource
                    };

            var categoriesConverted = new CategoryResourceConverter().ToEntity(categoriesResource);

            categoriesConverted.Should().Have.Count.EqualTo(2);

            AssertThis(categoriesConverted.First(), firstCategoryResource);
            AssertThis(categoriesConverted.Last(), secondCategoryResource);
        }

        private static void AssertThis(Entities.Category entity, Category resource)
        {
            entity.Id.Should().Be.EqualTo(resource.Id);
            entity.Name.Should().Be.EqualTo(resource.Name);
        }
    }
}
