using System.Collections.Generic;
using System.Linq;
using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Converters
{
    public class CategoryResourceConverter : ICategoryResourceConverter
    {
        public IEnumerable<Category> ToEntity(IEnumerable<Resources.Category> resources)
        {
            return resources.Select(category => new Category { Id = category.Id, Name = category.Name });
        }
    }
}