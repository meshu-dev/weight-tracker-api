using System.ComponentModel.DataAnnotations;
using WeightTracker.Api.Helpers;

namespace WeightTracker.Api.Models
{
    /// <summary>
    /// A weigh-in for a specific user 
    /// </summary>
    public class UserWeighInModel : WeighInModel
    {
        private readonly UserUnitConverter _userUnitConverter;

        /// <summary>
        /// Contructor used to create user weigh-ins
        /// </summary>
        public UserWeighInModel() : base()
        {
            _userUnitConverter = new UserUnitConverter(
                new UnitConverter()
            );
        }

        [Required]
        private string _value;

        /// <summary>
        /// The value of the weigh-in
        /// </summary>
        public override string Value {
            get
            {
                if (this.User != null)
                {
                    return _userUnitConverter
                    .ConvertToUserUnit(
                        this.User.UnitShortName,
                        _value
                    );
                }
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
