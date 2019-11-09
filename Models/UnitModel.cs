namespace WeightTracker.Api.Models
{
    /// <summary>
    /// A weight unit
    /// </summary>
    public class UnitModel : IModel
    {
        /// <summary>
        /// An id of the weight unit
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the weight unit
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The short name of the weight unit
        /// </summary>
        public string ShortName { get; set; }
    }
}
