namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IAuthorizationBuilder
    {
        string For(string login, string signature);
    }
}