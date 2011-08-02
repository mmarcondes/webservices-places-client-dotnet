using System;

namespace Maplink.Webservices.Places.Client.Wrappers
{
    public interface IClock
    {
        DateTime UtcNow();
    }
}