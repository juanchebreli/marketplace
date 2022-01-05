using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.UserDTO
{
    public class UserCreateDTO
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string lastname { get; set; }

        [Required]
        public string email { get; set; }

        public bool deleted { get; set; }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                configure(this);
            }
            public static void configure(Profile perfil)
            {
                perfil.CreateMap<User, UserCreateDTO>()
                    .ReverseMap();
            }
        }
    }
}
