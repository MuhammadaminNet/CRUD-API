using src.Services.DTOs;
using System.Threading.Tasks;

namespace src.yanabitta.Auth
{
    public interface IAuthService
    {
        ValueTask<string> LoginAsync(UserForLogin dto);
    }
}
