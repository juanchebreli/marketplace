using AutoMapper;
using marketplace.Models;

namespace marketplace.DTO.PaymentMethodDTO.CashMethodDTO
{
    public class CashMethodDTO : PaymentMethodDTO
    {
		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<CashMethod, CashMethodDTO>()
					.ReverseMap();
			}
		}
	}
}
