using AutoMapper;
using marketplace.Helpers.Enums;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.ProductOnSaleDTO
{
	public class ProductOnSaleCreateDTO
	{
		[Required]
		public decimal price { get; set; }

		[Required]
		public bool offer { get; set; }
		[Required]
		public int Productid { get; set; }
		[Required]
		public int Userid { get; set; }


		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<ProductOnSale, ProductOnSaleCreateDTO>()
					.ReverseMap()
					.ForMember(dest => dest.State, opt => opt.MapFrom(src => StatesEnum.FREE))
					.ForMember(dest => dest.deleted, opt => opt.MapFrom(src => false));

			}
		}
	}
}
