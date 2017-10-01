using System.ComponentModel.DataAnnotations;

namespace eKino.Infrastructure.Commands
{
    public class LoginCommand
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}