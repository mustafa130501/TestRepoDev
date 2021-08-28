using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {

        public string RefreshToken { get; set; }

        private readonly IBookContext _context;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(IBookContext context, IConfiguration configuration)
        {
            _context = context;
     
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x=>x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate>DateTime.Now);
            if (user is not null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Valid  Refresh Token  Not Found");
            }
        }

        public class CreateTokenModel
        {
            public string Password { get; set; }
            public string Email { get; set; }
        }
    }
}
