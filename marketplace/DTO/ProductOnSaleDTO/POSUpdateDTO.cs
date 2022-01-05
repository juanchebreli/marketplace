using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.ProductOnSaleDTO
{
	public class ProductOnSaleUpdateDTO
	{
		[Required]
		public int id { get; set; }
		[Required]
		public decimal price { get; set; }

		[Required]
		public bool offer { get; set; }

		public bool deleted { get; set; }

		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<ProductOnSale, ProductOnSaleUpdateDTO>()
					.ReverseMap();
			}
		}
	}
}
