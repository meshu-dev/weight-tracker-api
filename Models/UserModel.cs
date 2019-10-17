using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using WeightTracker.Api.Helpers;

namespace WeightTracker.Api.Models
{
    public class UserModel : IModel
    {
        public int Id { get; set; }

        public int UnitId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        private string _password;

        public string Password
        {
            get {
                return _password;
            }
            set {
                _password = Crypto.HashPassword(value);
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
