using AutoMapper;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Migrations
{
    #pragma warning disable CS1591
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Unit, UnitModel>()
                .ReverseMap();

            this.CreateMap<User, UserModel>()
                .ReverseMap();

            this.CreateMap<WeighIn, WeighInModel>()
                .ReverseMap();

            this.CreateMap<WeighIn, UserWeighInModel>()
                .ReverseMap();
        }
    }
    #pragma warning restore CS1591
}
