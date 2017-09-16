using System.ComponentModel.DataAnnotations;

namespace iKino.API.Requests
{
    public class CreateUser
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Mail { get; set; }
    }
}
