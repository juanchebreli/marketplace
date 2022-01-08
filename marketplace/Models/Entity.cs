using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace marketplace.Models
{
	public interface IEntity<T>
	{
		[Key]
		T id { get; set; }

		bool deleted { get; set; }
	}
	public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T id { get; set; }

		[Required]
		public bool deleted { get; set; }
	}
}
