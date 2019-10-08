using System;
using System.ComponentModel.DataAnnotations;

namespace WeightTracker.Api.Entities
{
    public class WeighIn
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
