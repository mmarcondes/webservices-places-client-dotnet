using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client
{
    public interface ICategorySearcher
    {
        IEnumerable<Category> All(LicenseInfo licenseInfo);
    }
}