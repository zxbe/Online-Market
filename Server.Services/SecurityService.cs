﻿namespace OnlineMarket.Server.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    public class SecurityService
    {
        private const string JwtSection = "JWT";
        public const string JwtSecurityKey = JwtSection + ":SecurityKey";
        public const string JwtIssuer = JwtSection + ":Issuer";
        public const string JwtAudience = JwtSection + ":Audience";
        public const string JwtExpiry = JwtSection + ":Expiry";

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

        public IEnumerable<Claim> GetClaims(IdentityUser user)
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

        public IEnumerable<Claim> GetClaims(IdentityUser user, IEnumerable<Claim> claims) 
        {
            return GetClaims(user).Concat(claims);
        }

        public SecurityToken GenerateJWT(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[JwtSecurityKey]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration[JwtIssuer],
              _configuration[JwtAudience],
              this.GetClaims(user),
              expires: DateTime.Now.AddHours(int.Parse(_configuration[JwtExpiry])),
              signingCredentials: credentials);

            return token;
        }

        public string GetTokenString(IdentityUser user) 
        {
            var token = this.GenerateJWT(user);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
