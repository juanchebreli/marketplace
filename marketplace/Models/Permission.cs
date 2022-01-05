using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.Models
{
	public class Permission : Entity<int>
	{
		[Required]
		public string name { get; set; }

		public virtual ICollection<Role> Roles { get; set; }

	}
}
