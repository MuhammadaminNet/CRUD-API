using Microsoft.AspNetCore.Mvc;
using src.Services.DTOs;
using src.yanabitta.Auth;
using System.Threading.Tasks;

namespace src.yanabitta.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> LoginAsync(UserForLogin dto) =>
            Ok(new
            {
                Token = await authService.LoginAsync(dto)
            });

    }
}

