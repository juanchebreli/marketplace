using AutoMapper;
using marketplace.Helpers.Enums;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.PaymentMethodDTO
{
	public class PaymentMethodCreateDTO
	{
		public string? description { get; set; }

		public string? bankName { get; set; }

		public string? cbu { get; set; }

		[Required]
		public PaymentMethodsEnum method { get; set; }

		public class MapperProfileCash : Profile
		{
			public MapperProfileCash()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<CashMethod, PaymentMethodCreateDTO>()
					.ReverseMap()
					.ForMember(dest => dest.deleted, opt => opt.MapFrom(src => false)) ;
			}
		}

		public class MapperProfileCard : Profile
		{
			public MapperProfileCard()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<CardMethod, PaymentMethodCreateDTO>()
					.ReverseMap()
					.ForMember(dest => dest.deleted, opt => opt.MapFrom(src => false));
			}
		}
	}
}

