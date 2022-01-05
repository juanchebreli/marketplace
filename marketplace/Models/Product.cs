using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.Models
{
	public class Product : Entity<int>
	{
		[Required]
		public string name { get; set; }

	}
}
