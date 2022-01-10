using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.ProductOnSaleDTO
{
	public class ProductOnSaleOfferDTO
	{
		public decimal price { get; set; }

		public string name { get; set; }

		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<ProductOnSale, ProductOnSaleOfferDTO>()
					.ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Product.name))
					.ReverseMap();
			}
		}
	}
}
