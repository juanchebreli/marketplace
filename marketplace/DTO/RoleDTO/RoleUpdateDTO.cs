using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.RoleDTO
{
	public class RoleUpdateDTO
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
				perfil.CreateMap<Role, RoleUpdateDTO>()
					.ReverseMap();
			}
		}
	}
}
