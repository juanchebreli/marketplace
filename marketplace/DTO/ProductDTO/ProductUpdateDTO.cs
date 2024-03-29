﻿using AutoMapper;
using marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.ProductDTO
{
	public class ProductUpdateDTO
	{
		[Required]
		public int id { get; set; }
		[Required]
		public string name { get; set; }

		[Required]
		public bool deleted { get; set; }

		public class MapperProfile : Profile
		{
			public MapperProfile()
			{
				configure(this);
			}
			public static void configure(Profile perfil)
			{
				perfil.CreateMap<Product, ProductUpdateDTO>()
					.ReverseMap();
			}
		}
	}
}
