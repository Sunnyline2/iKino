using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKino.Core.Domain
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Video> Videos { get; set; }


        public virtual ICollection<MovieActor> Actors { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}