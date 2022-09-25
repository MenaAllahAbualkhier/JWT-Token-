using AutoMapper;
using JWT.Extend;
using JWT.Model;

namespace JWT.Mapper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<ApplicationUser, RegistrationVM>();
            CreateMap<RegistrationVM, ApplicationUser>();

            CreateMap<ApplicationUser, LoginVM>();
            CreateMap<LoginVM, ApplicationUser>();
        }
    }
}
