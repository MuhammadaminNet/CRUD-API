using Data.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.DTOs;
using Services.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq.Expressions;
using Domain.Entities;

namespace yanabitta.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        /// <summary>
        /// Genereting JWT Barer token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="UserException"></exception>
        public async ValueTask<string> LoginAsync(UserForLogin dto)
        {
            var existUser = await unitOfWork.Users.GetAsync(
                u => u.Login == dto.Login && u.Password == dto.Password);

            if (existUser is null)
                throw new UserException(404, "Not Found");
            
            /// Genereting JWT Barer toket
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                                        configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: new Claim[]
                {
                    new Claim("Id", existUser.Id.ToString()),
                    new Claim(ClaimTypes.Role, existUser.Role.ToString())
                },
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(authSigningKey, 
                                                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
