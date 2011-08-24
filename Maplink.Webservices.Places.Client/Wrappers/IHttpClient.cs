namespace Maplink.Webservices.Places.Client.Wrappers
{
    public interface IHttpClient
    {
        HttpResponse Get(HttpRequest httpRequest);
    }
}