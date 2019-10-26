using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using WeightTracker.Api.Entities;

namespace WeightTracker.Api.Models
{
    public class UserModel : IModel
    {
        public int Id { get; set; }

        [Required]
        public int UnitId { get; set; }

        public string UnitName { get; set; }

        public string UnitShortName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        private string _password;

        public string Password
        {
            get => _password;
            set => _password = Crypto.HashPassword(value);
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
