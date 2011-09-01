using System;
using System.Net;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Exceptions
{
    public class PlaceClientRequestException : Exception
    {
        private const string MessageTemplate
            = "An error occurred while requesting the webservice. The response was {0} {1} - {2}";

        public PlaceClientRequestException(HttpResponse response)
            : base(
                String.Format(
                    MessageTemplate,
                    response.StatusCode,
                    (HttpStatusCode)response.StatusCode,
                    response.Body))
        {
        }
    }
}