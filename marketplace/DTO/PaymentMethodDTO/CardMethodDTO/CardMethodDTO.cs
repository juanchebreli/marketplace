using AutoMapper;
using marketplace.Models;

namespace marketplace.DTO.PaymentMethodDTO.CardMethodDTO
{
    public class CardMethodDTO: PaymentMethodDTO
    {
        public string? bankName { get; set; }
        public string? cbu { get; set; }

		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<CardMethod, CardMethodDTO>()
					.ReverseMap();
			}
		}
	}
}
