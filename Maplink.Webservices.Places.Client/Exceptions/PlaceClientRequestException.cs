using System;

namespace Maplink.Webservices.Places.Client.Exceptions
{
    public class PlaceClientRequestException : Exception
    {
        private const string MessageTemplate 
            = "An error occurred while requesting the webservice. The response status code was {0}";
        
        public PlaceClientRequestException(int statusCode)
            : base(String.Format(MessageTemplate, statusCode))
        {
        }
    }
}