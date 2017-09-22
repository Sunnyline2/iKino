using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iKino.API.Domain
{
    public class Movie
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MovieId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public TimeSpan Duration { get; protected set; }
        public virtual ICollection<MovieActor> MovieActors { get; protected set; }

        public Movie(string name, string description, TimeSpan duration)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can not be empty.", nameof(name));
        
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description can not be empty.", nameof(description));


            this.Name = name;
            this.Description = description;
            this.Duration = duration;
        }
        protected Movie()
        {
        }

    }
}