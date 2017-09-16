using System.ComponentModel.DataAnnotations;

namespace iKino.API.Requests
{
    public class Authenticate
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
