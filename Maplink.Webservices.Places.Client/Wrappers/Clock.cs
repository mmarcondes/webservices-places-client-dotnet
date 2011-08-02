using System;

namespace Maplink.Webservices.Places.Client.Wrappers
{
    public class Clock : IClock
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
