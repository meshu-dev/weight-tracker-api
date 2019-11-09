namespace WeightTracker.Api.Models
{
    /// <summary>
    /// Model representing the login details for a user account
    /// </summary>
    public class AuthModel
    {
        /// <summary>
        /// The e-mail address of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; }
    }
}
