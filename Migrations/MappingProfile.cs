using AutoMapper;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Migrations
{
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
        }
    }
}
