using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.Models
{
    public class User :  Entity<int>
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string lastname { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
		public int Roleid { get; set; }

		public virtual Role Role { get; set; }

		public virtual ICollection<ProductOnSale> ProductOnSales { get; set; }

		public virtual ICollection<Purchase> Purchases { get; set; }
	}
}
