using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.PaymentMethodDTO.CardMethodDTO
{
	public class CardMethodUpdateDTO
	{
		[Required]
		public int id { get; set; }
		public string description { get; set; }
		[Required]
		public string bankName { get; set; }
		[Required]
		public string cbu { get; set; }

		public bool deleted { get; set; }

		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<CardMethod, CardMethodUpdateDTO>()
					.ReverseMap();
			}
		}
	}
}

