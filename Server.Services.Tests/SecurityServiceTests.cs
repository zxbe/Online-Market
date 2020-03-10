using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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

        [Test]
        public void GetTokenValidationParametersExists()
        {

            Assert.NotNull(this.ServicesAssembly
                .GetTypes()
                .SingleOrDefault(t => t.Name == "SecurityService")
                .GetMethods()
                .SingleOrDefault(mi =>
                    mi.Name == "GetTokenValidationParameters"));
        }

        [Test]
        public void GetTokenValidationParametersIsStatic()
        {
            Assert.NotNull(this.ServicesAssembly
                .GetTypes()
                .SingleOrDefault(t => t.Name == "SecurityService")
                .GetMethods()
                .SingleOrDefault(mi =>
                    mi.Name == "GetTokenValidationParameters"
                    && mi.IsStatic));
        }

        [Test]
        public void GetTokenValidationParametersArgumentTest()
        {
            Assert.NotNull(this.ServicesAssembly
                .GetTypes()
                .SingleOrDefault(t => t.Name == "SecurityService")
                .GetMethods()
                .SingleOrDefault(mi =>
                    mi.Name == "GetTokenValidationParameters")
                .GetParameters()
                .SingleOrDefault(pi =>
                    pi.ParameterType == typeof(IConfiguration)));
        }

        [Test]
        public void GetTokenValidationParametersReturnTypeTest()
        {
            Assert.NotNull(this.ServicesAssembly
                .GetTypes()
                .SingleOrDefault(t => t.Name == "SecurityService")
                .GetMethods()
                .SingleOrDefault(mi =>
                    mi.Name == "GetTokenValidationParameters")
                .ReturnType == typeof(TokenValidationParameters));
        }
    }
}