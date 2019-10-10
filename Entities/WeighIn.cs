using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeightTracker.Api.Entities
{
    public class WeighIn
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
