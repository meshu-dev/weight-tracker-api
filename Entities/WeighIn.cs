using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeightTracker.Api.Entities
{
    #pragma warning disable CS1591
    public class WeighIn : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
    #pragma warning restore CS1591
}
