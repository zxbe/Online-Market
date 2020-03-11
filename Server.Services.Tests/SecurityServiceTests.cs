using System.Text;

namespace OnlineMarket.Server.Services.Tests
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;

    public class SecurityServiceTests
    {
        private const string appsettingsJsonFile = "./../../../../Server/appsettings.json";
        private Assembly ServicesAssembly { get; set; }
        public IConfiguration Configuration { get; set; }

        public SecurityServiceTests()
        {
          var appsettingsFileName = Directory.GetCurrentDirectory() + appsettingsJsonFile;
            this.Configuration = new ConfigurationBuilder()
                .AddJsonFile(appsettingsFileName)
                .AddEnvironmentVariables()
                .Build();

            this.ServicesAssembly = Assembly.Load("OnlineMarket.Server.Services");
        }

        [SetUp]
        public void Setup()
        {
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

        //[Test]
        //public void ConstructorTests()
        //{
        //    Assert.Pass();
        //}

        [Test]
        public void GetTokenValidationParametersTests() 
        {
            var tvp = SecurityService.GetTokenValidationParameters(this.Configuration);

            Assert.That(tvp.ValidateIssuer);
            Assert.That(tvp.ValidateAudience);
            Assert.That(tvp.ValidateLifetime);
            Assert.AreEqual(this.Configuration[SecurityService.JwtIssuer], tvp.ValidIssuer);
            Assert.AreEqual(this.Configuration[SecurityService.JwtAudience], tvp.ValidAudience);
        }
    }
}