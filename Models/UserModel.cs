using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using WeightTracker.Api.Entities;

namespace WeightTracker.Api.Models
{
    /// <summary>
    /// A user account
    /// </summary>
    public class UserModel : IModel
    {
        /// <summary>
        /// An id for the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// An id for the user role
        /// </summary>
        [Required]
        public int RoleId { get; set; }

        /// <summary>
        /// The name of the user role
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// An unit id for the user
        /// </summary>
        [Required]
        public int UnitId { get; set; }

        /// <summary>
        /// The name of the weight unit for the user
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// The short name of the weight unit for the user
        /// </summary>
        public string UnitShortName { get; set; }

        /// <summary>
        /// The e-mail address of the user
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        [Required]
        public string Password { get; set; }

        public string Fullname
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        /// <summary>
        /// The first name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; }
    }
}
