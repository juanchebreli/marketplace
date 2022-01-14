using AutoMapper;
using marketplace.DTO.PaymentMethodDTO;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.PurchaseDTO
{
	public class PurchaseCreateDTO
	{
		[Required]
		public decimal amount { get; set; }

		[Required]
		public int Userid { get; set; }


		[Required]
		public int ProductOnSaleid { get; set; }


		public PaymentMethodCreateDTO paymentMethod { get; set; }

		public int PaymentMethodid { get; set; }


		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<Purchase, PurchaseCreateDTO>()
					.ReverseMap()
					.ForMember(dest => dest.deleted, opt => opt.MapFrom(src => false))
					.ForMember(dest => dest.date, opt => opt.MapFrom(src => DateTime.Now))
					.ForMember(x => x.PaymentMethod, opt => opt.Ignore());
			}
		}
	}
}
