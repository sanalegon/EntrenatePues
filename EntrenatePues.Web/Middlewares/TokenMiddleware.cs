using EntrenatePues.Core.Interfaces.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace EntrenatePues.Web.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="configuration"></param>
        public TokenMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;

        }

        /// <summary>
        /// Search for the header token and if it is null it returns an unauthorized, otherwise it validates the token
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        /// <param name="loginSesionService"></param>
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await attachUserToContextAsync(context, token, userService);
            }

            await _next(context);
        }


        /// <summary>
        /// Validate token parameters
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <param name="userService"></param>
        /// <param name="loginSesionService"></param>
        private async Task attachUserToContextAsync(HttpContext context, string token, IUserService userService)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                byte[] key = Encoding.ASCII.GetBytes(_configuration["Tokens:SecretKey"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Tokens:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = _configuration["Tokens:Audience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                string id = (jwtToken.Claims.First(x => x.Type == "Id").Value);
                string username = (jwtToken.Claims.First(x => x.Type == "UserName").Value);
                //validate service!!!

                // attach user to context on successful jwt validation

                context.Items["UserName"] = username;
                context.Items["User"] = userService.FindUserByUserId(int.Parse(id));
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
