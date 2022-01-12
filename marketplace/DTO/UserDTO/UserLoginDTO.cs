using AutoMapper;
using marketplace.Models;
namespace marketplace.DTO.UserDTO
{
    public class UserLoginDTO
    {
        public string token { get; set; }

        public string username { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }

        public string email { get; set; }
        public string role { get; set; }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                configure(this);
            }
            public static void configure(Profile perfil)
            {
                perfil.CreateMap<User, UserLoginDTO>()
					.ForMember(dest => dest.role, opt => opt.MapFrom(src => src.Role.name))
					.ReverseMap();
            }
        }

    }
}
