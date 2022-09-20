using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.PurchaseDTO
{
	public class PurchaseUpdateDTO
	{
		[Required]
		public int id { get; set; }
		[Required]
		public decimal amount { get; set; }

		[Required]
		public int Userid { get; set; }


		[Required]
		public int ProductOnSaleid { get; set; }

		[Required]
		public string PaymentMethod { get; set; }


		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<Purchase, PurchaseUpdateDTO>()
					.ReverseMap();
			}
		}
	}
}
