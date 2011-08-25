using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Converters
{
    public interface ICategoryResourceConverter
    {
        IEnumerable<Category> ToEntity(IEnumerable<Client.Resources.Category> resources);
    }
}