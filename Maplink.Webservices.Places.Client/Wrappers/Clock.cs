using System;

namespace Maplink.Webservices.Places.Client.Wrappers
{
    public class Clock : IClock
    {
        public DateTime UtcHourNow()
        {
            var now = DateTime.Now;

            return 
                new DateTime(
                    now.Year, 
                    now.Month, 
                    now.Day, 
                    now.Hour, 
                    1, 
                    1)
                .ToUniversalTime();
        }
    }
}
