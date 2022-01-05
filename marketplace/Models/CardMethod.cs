using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.Models
{
	public class CardMethod : PaymentMethod
	{
		[Required]
		public string bankName { get; set; }
		[Required]
		public string cbu { get; set; }

	}
}
