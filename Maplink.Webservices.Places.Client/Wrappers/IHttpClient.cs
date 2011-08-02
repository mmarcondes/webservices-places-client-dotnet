namespace Maplink.Webservices.Places.Client.Wrappers
{
    public interface IHttpClient
    {
        Response Get(Request request);
    }
}