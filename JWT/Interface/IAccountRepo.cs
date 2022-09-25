using JWT.Helper;
using JWT.Model;

namespace JWT.Interface
{
    public interface IAccountRepo
    {
        Task<APIResponse<object>> Regisration(RegistrationVM model);
        Task<APIResponse<object>> Login(LoginVM model);
    }
}
