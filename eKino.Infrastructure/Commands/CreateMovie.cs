using System;
using System.ComponentModel.DataAnnotations;

namespace eKino.Infrastructure.Commands
{
    public class CreateMovie
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }
    }
}