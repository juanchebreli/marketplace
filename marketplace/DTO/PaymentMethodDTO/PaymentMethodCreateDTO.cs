using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.PaymentMethodDTO
{
	public class PaymentMethodCreateDTO
	{
		public string description { get; set; }
		public string bankName { get; set; }
		public string cbu { get; set; }

		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<PaymentMethod, PaymentMethodCreateDTO>()
					.ReverseMap()
					.ForMember(dest => dest.deleted, opt => opt.MapFrom(src => false));
			}
		}
	}
}
