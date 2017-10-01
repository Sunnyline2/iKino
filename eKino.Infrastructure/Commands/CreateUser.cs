using System.ComponentModel.DataAnnotations;

namespace eKino.Infrastructure.Commands
{
    public class CreateUser
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Mail { get; set; }
    }
}