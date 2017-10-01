using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKino.Core.Domain
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid VoteId { get; set; }

        public decimal Rate { get; set; }
        public string Description { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}