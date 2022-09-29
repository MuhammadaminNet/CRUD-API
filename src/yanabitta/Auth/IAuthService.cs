using Services.DTOs;
using System.Threading.Tasks;

namespace yanabitta.Auth
{
    public interface IAuthService
    {
        Task<string> LoginAsync(UserForLogin dto);
    }
}
