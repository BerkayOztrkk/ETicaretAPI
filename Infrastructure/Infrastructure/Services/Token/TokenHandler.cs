﻿using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int minute,AppUser appUser)
        {
            Application.DTOs.Token token = new();

            //Security keyin simetriğini alınıyor
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //şifrelenmiş key oluşturuluyor
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //token ayarları oluşturuluyor
            token.Expiration=DateTime.UtcNow.AddMinutes(190);
            JwtSecurityToken securityToken = new(

           audience: _configuration["Token:Audience"],
           issuer: _configuration["Token:Issuer"],
           expires: token.Expiration.AddMinutes(190),
           notBefore: DateTime.UtcNow,
           signingCredentials:signingCredentials,
           claims:new List<Claim> { new(ClaimTypes.Name,appUser.UserName)}

           

            );
            //Token oluşturucu sınıfından bir örnek oluşturuluyor
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken=tokenHandler.WriteToken(securityToken);
           
            token.RefreshToken=CreateRefreshToken();
            return token;

        }

        public string CreateRefreshToken()
        {
            byte[] number =new byte[32];
            using RandomNumberGenerator random=RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);

        }
    }
}