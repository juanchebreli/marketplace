using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.PermissionDTO
{
	public class PermissionUpdateDTO
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
				perfil.CreateMap<Permission, PermissionUpdateDTO>()
					.ReverseMap();
			}
		}
	}
}
