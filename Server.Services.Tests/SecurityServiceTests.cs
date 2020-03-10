namespace OnlineMarket.Server.Services.Tests
{
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;

    public class SecurityServiceTests
    {
        private Assembly ServicesAssembly { get; set; }

        [SetUp]
        public void Setup()
        {
            this.ServicesAssembly = Assembly.Load("OnlineMarket.Server.Services");
        }

        [Test]
        public void SecurityServiceExists()
        {
            Assert.Contains(typeof(SecurityService), this.ServicesAssembly.GetTypes());
        }
    }
}