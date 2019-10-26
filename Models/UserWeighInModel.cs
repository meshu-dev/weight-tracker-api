using System;
using System.ComponentModel.DataAnnotations;
using WeightTracker.Api.Helpers;

namespace WeightTracker.Api.Models
{
    public class UserWeighInModel : WeighInModel
    {
        private UserUnitConverter _userUnitConverter;

        public UserWeighInModel() : base()
        {
            _userUnitConverter = new UserUnitConverter(
                new UnitConverter()
            );
        }

        [Required]
        private string _value;

        public override string Value {
            get
            {
                return _userUnitConverter
                    .ConvertToUserUnit(
                        this.User.UnitShortName,
                        _value
                    );
            }
            set
            {
                if (this.User != null)
                {
                    _value = _userUnitConverter
                        .ConvertToBaseUnit(
                            this.User.UnitShortName,
                            value
                        );
                }
                else
                {
                    _value = value;
                }
            }
        }
    }
}
