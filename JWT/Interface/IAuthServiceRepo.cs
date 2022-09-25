using JWT.Extend;
using System.IdentityModel.Tokens.Jwt;

namespace JWT.Interface
{
    public interface IAuthServiceRepo
    {
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
    }
}
