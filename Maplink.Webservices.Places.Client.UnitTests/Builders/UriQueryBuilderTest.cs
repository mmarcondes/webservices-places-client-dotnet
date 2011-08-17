using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Builders
{
    [TestClass]
    public class UriQueryBuilderTest
    {
        private IUriQueryBuilder _uriQueryBuilder;
        private IEnumerable<KeyValuePair<string,string>> _arguments;

        [TestInitialize]
        public void SetUp()
        {
            _uriQueryBuilder = new UriQueryBuilder();
        }

        [TestMethod]
        public void ShouldBuildUriQuery()
        {
            _arguments = new Dictionary<string, string>
                                {
                                    {"arg1", "value1"},
                                    {"arg2", "value2"}
                                };
            
            _uriQueryBuilder.Build(_arguments).Should().Be.EqualTo("arg1=value1&arg2=value2");
        }

        [TestMethod]
        public void ShouldBuildEncodedUriQuery()
        {
            _arguments = new Dictionary<string, string>
                                {
                                    {"argument 1", "value1"},
                                    {"argument2", "value 2"}
                                };

            _uriQueryBuilder.Build(_arguments)
                .Should().Be.EqualTo("argument+1=value1&argument2=value+2");
        }
    }
}
