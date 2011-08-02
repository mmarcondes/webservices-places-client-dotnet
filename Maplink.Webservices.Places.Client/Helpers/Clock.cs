using System;

namespace Maplink.Webservices.Places.Client.Helpers
{
    public class Clock : IClock
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
