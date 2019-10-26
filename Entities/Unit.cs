using System.ComponentModel.DataAnnotations;

namespace WeightTracker.Api.Entities
{
    public class Unit : IEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [StringLength(10, MinimumLength = 2)]
        public string ShortName { get; set; }
    }
}
