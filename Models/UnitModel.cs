using System;

namespace WeightTracker.Api.Models
{
    public class UnitModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
