using System;
using System.ComponentModel.DataAnnotations;

namespace WeightTracker.Api.Models
{
    public class WeighInModel
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
