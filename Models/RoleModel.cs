namespace WeightTracker.Api.Models
{
    /// <summary>
    /// A user role
    /// </summary>
    public class RoleModel : IModel
    {
        /// <summary>
        /// An id of the user role
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the user role
        /// </summary>
        public string Name { get; set; }
    }
}
