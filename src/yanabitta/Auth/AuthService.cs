using Data.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.DTOs;
using Services.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<string> LoginAsync(UserForLogin dto)
        {
            var existUser = await unitOfWork.Users.GetAsync(
                u => u.Login == dto.Login && u.Password == dto.Password);

            if (existUser is null)
                throw new UserException(404, "Not Found");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));


            string mainToken = new JwtSecurityTokenHandler().WriteToken(token);

            return mainToken;
        }
    }
}
