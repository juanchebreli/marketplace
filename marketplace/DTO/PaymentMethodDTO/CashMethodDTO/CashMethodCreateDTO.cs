using AutoMapper;
using marketplace.Helpers.Enums;
using marketplace.Models;

namespace marketplace.DTO.PaymentMethodDTO.CashMethodDTO
{
	public class CashMethodCreateDTO
	{

		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<CashMethod, CashMethodCreateDTO>()
					.ReverseMap()
					.ForMember(dest => dest.deleted, opt => opt.MapFrom(src => false))
					.ForMember(dest => dest.type, opt => opt.MapFrom(src => PaymentMethodsEnum.Cash));
			}
		}
	}
}

