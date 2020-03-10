namespace OnlineMarket.Server.Services.Tests
{
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;

    public class SecurityServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SecurityServiceExists()
        {
            Assert.Contains(
                typeof(SecurityService),
                Assembly
                    .GetExecutingAssembly()
                    .GetTypes());
        }
    }
}