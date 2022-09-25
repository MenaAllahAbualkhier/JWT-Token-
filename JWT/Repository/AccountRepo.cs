using AutoMapper;
using JWT.Database;
using JWT.Extend;
using JWT.Helper;
using JWT.Interface;
using JWT.Model;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace JWT.Repository
{
    public class AccountRepo : IAccountRepo
    {

        #region Costr
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAuthServiceRepo iAuthServiceRepo;
        private readonly DemoContext db;

        public AccountRepo(IMapper mapper, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, IAuthServiceRepo iAuthServiceRepo,
            DemoContext db)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.iAuthServiceRepo = iAuthServiceRepo;
            this.db = db;
        }
        #endregion
        public async Task<APIResponse<object>> Regisration(RegistrationVM model)
        {
            try
            {
                var user = mapper.Map<ApplicationUser>(model);
                user.UserName = model.Mobile;
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var result2 = await userManager.AddToRoleAsync(user, "User");
                    return new APIResponse<object>
                    {
                        Code = "200",
                        State = "Ok ",
                        Message = "Registration successful",
                        Data = new { Mobile = model.Mobile, FirstName = model.FirstName }
                    };
                }
                else
                {
                    var error = string.Empty;
                    if (result.ToString() == "Failed : DuplicateUserName")
                    {
                        error = "Phone Number is already used";
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            error += " " + item.Description;
                        }
                    }
                    return new APIResponse<object>()
                    {
                        Code = "400",
                        State = "BadRequist",
                        Message = "Faild",
                        Error = error
                    };
                }
            }
            catch (Exception ex)
            {
                return new APIResponse<object>()
                {
                    Code = "404",
                    State = "NotFound",
                    Message = "Faild",
                    Error = ex.Message
                };
            }

        }


        public async Task<APIResponse<object>> Login(LoginVM model)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(model.Mobile, model.Password, true, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(model.Mobile);
                    var rolename = await userManager.GetRolesAsync(user);
                    var jwtSecurityToken = await iAuthServiceRepo.CreateJwtToken(user);

                    return new APIResponse<object>
                    {
                        Code = "200",
                        State = "Ok",
                        Message = "Login successful",
                        Data = new
                        {
                            Id = user.Id,
                            Phone = user.UserName,
                            UserName = user.FirstName + " "+user.LastName,
                            RoleName = rolename.FirstOrDefault(),
                            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                        }
                    };
                }

                else
                {
                    return new APIResponse<object>()
                    {
                        Code = "400",
                        State = "BadRequist",
                        Message = "Faild",
                        Error = "not correct phone number or password"
                    };
                }

            }
            catch (Exception ex)
            {
                return new APIResponse<object>()
                {
                    Code = "404",
                    State = "NotFound",
                    Message = "Faild",
                    Error = ex.Message
                };
            }


        }


    }
}
