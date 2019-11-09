using System.ComponentModel.DataAnnotations;
using System;

namespace WeightTracker.Api.Models
{
    /// <summary>
    /// A weigh-in measurement for a specific user
    /// </summary>
    public class WeighInModel : IModel
    {
        /// <summary>
        /// The first name of the author
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Id for the user associated with the weigh-in
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// The value of the weigh-in
        /// </summary>
        [Required]
        public virtual string Value { get; set; }

        /// <summary>
        /// The date of the weigh-in
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// The user associated with the weigh-in
        /// </summary>
        public UserModel User { protected get; set; }
    }
}
