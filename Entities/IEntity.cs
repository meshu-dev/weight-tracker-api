using System.ComponentModel.DataAnnotations;

namespace WeightTracker.Api.Entities
{
    public abstract class IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
