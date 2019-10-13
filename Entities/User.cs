using System;
using System.ComponentModel.DataAnnotations;

namespace WeightTracker.Api.Entities
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
