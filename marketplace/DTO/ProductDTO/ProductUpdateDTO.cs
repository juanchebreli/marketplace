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
	}
}
