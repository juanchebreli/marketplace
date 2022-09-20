using AutoMapper;
using marketplace.Helpers.Enums;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.PaymentMethodDTO.CardMethodDTO
{
	public class CardMethodCreateDTO
	{
		public string description { get; set; }
		[Required]
		public string bankName { get; set; }
		[Required]
		public string cbu { get; set; }

		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<CashMethod, CardMethodCreateDTO>()
					.ReverseMap()
					.ForMember(dest => dest.deleted, opt => opt.MapFrom(src => false))
					.ForMember(dest => dest.type, opt => opt.MapFrom(src => PaymentMethodsEnum.Card));
			}
		}
	}
}

