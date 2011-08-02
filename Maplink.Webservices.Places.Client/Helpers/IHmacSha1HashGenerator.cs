namespace Maplink.Webservices.Places.Client.Helpers
{
    public interface IHmacSha1HashGenerator
    {
        string For(string content, string key);
    }
}