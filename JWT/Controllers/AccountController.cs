using JWT.Helper;
using JWT.Interface;
using JWT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo accountRepo;
        #region Cost
        public AccountController(IAccountRepo accountRepo)
        {
            this.accountRepo = accountRepo;
        }

        #endregion


        #region Registration
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            var result= await accountRepo.Regisration(model);
            if (result.Code == "200")
            {
                return Ok(result);
            }
            else if (result.Code == "400")
            {
                return BadRequest(result);
            }
            else
            {
                return NotFound(result);
            }

        }
        #endregion

        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var result = await accountRepo.Login(model);
            if (result.Code == "200")
            {
                return Ok(result);
            }
            else if (result.Code == "400")
            {
                return BadRequest(result);
            }
            else
            {
                return NotFound(result);
            }

        }
        #endregion
    }
}
