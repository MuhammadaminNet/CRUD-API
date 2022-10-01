using Services.DTOs;
using System.Threading.Tasks;

namespace yanabitta.Auth
{
    public interface IAuthService
    {
        ValueTask<string> LoginAsync(UserForLogin dto);
    }
}
