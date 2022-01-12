using AutoMapper;
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
					.ForMember(dest => dest.state, opt => opt.MapFrom(src => State.FREE.id))
					.ForMember(dest => dest.deleted, opt => opt.MapFrom(src => false));

			}
		}
	}
}
