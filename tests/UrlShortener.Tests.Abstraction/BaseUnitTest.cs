using AutoFixture;

namespace UrlShortener.Tests.Abstraction
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