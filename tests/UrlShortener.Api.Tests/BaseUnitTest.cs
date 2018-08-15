using AutoFixture;

namespace UrlShortener.Api.Tests
{
    public class BaseUnitTest
    {
        protected readonly IFixture Fixture;

        public BaseUnitTest()
        {
            Fixture = new Fixture();
        }
    }
}