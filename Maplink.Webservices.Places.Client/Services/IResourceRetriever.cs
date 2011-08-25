using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Resources;

namespace Maplink.Webservices.Places.Client.Services
{
    public interface IResourceRetriever
    {
        TResource From<TResource>(Request request) where TResource : Resource, new();
    }
}