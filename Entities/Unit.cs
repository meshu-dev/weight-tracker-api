using System;
using System.ComponentModel.DataAnnotations;

namespace WeightTracker.Api.Entities
{
    public class Unit
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
