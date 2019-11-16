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

        /*
        /// <summary>
        /// An role id for the user
        /// </summary>
        private int _roleId { get; set; }

        [Required]
        [EnumDataType(typeof(Role))]
        public virtual Role Role
        {
            get
            {
                return (Role) this._roleId;
            }
            set
            {
                _roleId = (int) value;
            }
        } */

        //public int RoleId { get; set; }

        public Role Role { get; set; }

        /// <summary>
        /// A role type for the user
        /// </summary>
        //[EnumDataType(typeof(Role))]
        //public Role Role { get; set; }

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

        /// <summary>
        /// The first name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; }
    }

    public enum Role
    {
        Admin = 1,
        Standard = 2
    }
}
