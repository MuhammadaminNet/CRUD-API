using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using System.Threading.Tasks;
using yanabitta.Auth;

namespace yanabitta.Controllers
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

