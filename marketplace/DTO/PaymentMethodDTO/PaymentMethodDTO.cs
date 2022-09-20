using AutoMapper;
using marketplace.Models;

namespace marketplace.DTO.PaymentMethodDTO
{
    public abstract class PaymentMethodDTO
    {
		public int id { get; set; }
		public string? description { get; set; }
		public virtual Purchase Purchase { get; set; }

	}
}
