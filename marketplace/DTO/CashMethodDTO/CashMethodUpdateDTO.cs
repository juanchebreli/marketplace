using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.CashMethodDTO
{
	public class CashMethodUpdateDTO
	{
		[Required]
		public int id { get; set; }
		public string description { get; set; }

		public bool deleted { get; set; }

		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<CashMethod, CashMethodUpdateDTO>()
					.ReverseMap();
			}
		}
	}
}

