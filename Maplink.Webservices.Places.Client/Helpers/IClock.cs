using System;

namespace Maplink.Webservices.Places.Client.Helpers
{
    public interface IClock
    {
        DateTime UtcNow();
    }
}