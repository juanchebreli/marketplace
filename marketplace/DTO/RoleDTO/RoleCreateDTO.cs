using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.RoleDTO
{
	public class RoleCreateDTO
	{
		[Required]
		public string name { get; set; }

		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<Role, RoleCreateDTO>()
					.ReverseMap()
					.ForMember(dest => dest.deleted, opt => opt.MapFrom(src => false));
			}
		}
	}
}
