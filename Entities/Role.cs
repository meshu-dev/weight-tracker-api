using System.ComponentModel.DataAnnotations;

namespace WeightTracker.Api.Entities
{
#pragma warning disable CS1591
    public class Role : IEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
#pragma warning restore CS1591
}
