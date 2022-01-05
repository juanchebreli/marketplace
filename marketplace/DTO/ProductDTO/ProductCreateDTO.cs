using System.ComponentModel.DataAnnotations;

namespace marketplace.DTO.ProductDTO
{
	public class ProductCreateDTO
	{
		[Required]
		public string name { get; set; }

		public bool deleted { get; set; }
	}
}
