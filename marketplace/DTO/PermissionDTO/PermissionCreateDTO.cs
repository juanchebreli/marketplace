using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.PermissionDTO
{
	public class PermissionCreateDTO
	{
		[Required]
		public int id { get; set; }
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
				perfil.CreateMap<Permission, PermissionCreateDTO>()
					.ReverseMap()
					.ForMember(dest => dest.deleted, opt => opt.MapFrom(src => false));
			}
		}
	}
}
