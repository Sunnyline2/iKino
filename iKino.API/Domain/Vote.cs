using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iKino.API.Domain
{
    public class Vote
    {
        protected Vote()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid VoteId { get; protected set; }


        public decimal Rate { get; protected set; }
        public string Description { get; protected set; }



        public Vote(string description, decimal rate)
        {
            if (rate < 0 || rate > 10)
                throw new ArgumentException("Wrong rate", nameof(rate));

            Description = description;
            Rate = rate;
        }
    }
}