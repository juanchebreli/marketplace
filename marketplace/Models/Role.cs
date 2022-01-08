using System.ComponentModel.DataAnnotations;

namespace marketplace.Models
{
	public class Role : Entity<int>
	{
		[Required]
		public string name { get; set; }

		public virtual ICollection<User> Users { get; set; }
		public virtual ICollection<Permission> Permissions { get; set; }

	}
}
