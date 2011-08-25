using Maplink.Webservices.Places.Client.Resources;

namespace Maplink.Webservices.Places.Client.UnitTests.Services
{
    public class DummyResource : Resource
    {
        public DummyResource()
        {
            DummyProperty = "default-value";
        }

        public string DummyProperty { get; set; }

        private bool Equals(DummyResource other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Equals(other.DummyProperty, DummyProperty);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (DummyResource) && Equals((DummyResource) obj);
        }

        public override int GetHashCode()
        {
            return (DummyProperty != null ? DummyProperty.GetHashCode() : 0);
        }

        public static bool operator ==(DummyResource left, DummyResource right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DummyResource left, DummyResource right)
        {
            return !Equals(left, right);
        }
    }
}