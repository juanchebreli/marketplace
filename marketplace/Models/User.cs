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
        public bool deleted { get; set; }

        [Required]
        public string role { get; set; }
        // Admin, User, Mod
    }
}
