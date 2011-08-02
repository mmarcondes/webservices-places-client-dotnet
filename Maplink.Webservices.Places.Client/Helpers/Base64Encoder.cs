using System;
using System.Text;

namespace Maplink.Webservices.Places.Client.Helpers
{
    public class Base64Encoder : IBase64Encoder
    {
        public string Encode(string aBase64String)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(aBase64String));
        }
    }
}
