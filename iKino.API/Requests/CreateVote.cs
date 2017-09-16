using System.ComponentModel.DataAnnotations;

namespace iKino.API.Requests
{
    public class CreateVote
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Rate { get; set; }
    }
}
