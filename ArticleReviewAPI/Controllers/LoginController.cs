using ArticleReviewAPI.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleReviewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Authentication for the api
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>Token values</returns>
        [HttpPost("action")]
        public Token Login([FromForm] UserLogin userLogin)
        {
            User user = InMemoryUserLogins.Users.FirstOrDefault(x => x.Email == userLogin.Email && x.Password == userLogin.Password);
            if (user == null)
                return null;

            TokenHandler tokenHandler = new TokenHandler(_configuration);
            Token token = tokenHandler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenEndDate = token.Expiration.AddMinutes(TokenHandler.RefreshTokenExpireMinute);

            return token;
        }
        /// <summary>
        /// refresh the token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns>Token values</returns>
        [HttpPost("[action]")]
        public Token RefreshTokenLogin([FromForm] string refreshToken)
        {
            User user = InMemoryUserLogins.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.Now)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(TokenHandler.RefreshTokenExpireMinute);

                return token;
            }
            return null;
        }
    }
}
