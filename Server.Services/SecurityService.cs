﻿namespace OnlineMarket.Server.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Text;

    public class SecurityService
    {
        private const string JwtSection = "JWT";
        private const string JwtSecurityKey = JwtSection + ":SecurityKey";
        private const string JwtIssuer = JwtSection + ":Issuer";
        private const string JwtAudience = JwtSection + ":Audience";
        private const string JwtExpiry = JwtSection + ":Expiry";

        private readonly IConfiguration _configuration;

        public SecurityService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            var Issuer = configuration[JwtIssuer];
            var Audience = configuration[JwtAudience];
            var KeyString = configuration[JwtSecurityKey];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeyString));

            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = Key
            };
        }

        public Claim[] GetClaims(IdentityUser user)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber != null
                    ?user.PhoneNumber
                    :string.Empty)
            };
        }
    }
}
