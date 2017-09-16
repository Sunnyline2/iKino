using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iKino.API.Domain
{
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ActorId { get; protected set; }
        public string Name { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public DateTime DateOfDeath { get; protected set; }

        public virtual ICollection<MovieActor> MovieActors { get; protected set; }

        public Actor(string name, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can not be empty.", nameof(name));
            if (dateOfBirth >= DateTime.UtcNow)
                throw new ArgumentException("The date given is incorrect.", nameof(dateOfBirth));

            this.Name = name;
            this.DateOfBirth = dateOfBirth;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can not be empty.", nameof(name));

            this.Name = name;
        }

        public void SetDeath(DateTime dateTime)
        {
            this.DateOfDeath = dateTime;
        }

        public void SetBirth(DateTime dateTime)
        {
            this.DateOfBirth = dateTime;
        }

        protected Actor()
        {
        }
    }
}