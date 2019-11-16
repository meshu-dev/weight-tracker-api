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

            /*
            this.CreateMap<User, UserModel>()
                .ReverseMap(); */

            this.CreateMap<User, UserModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (Role) src.RoleId));

            this.CreateMap<UserModel, User>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => (int) src.Role));

            this.CreateMap<WeighIn, WeighInModel>()
                .ReverseMap();

            this.CreateMap<WeighIn, UserWeighInModel>()
                .ReverseMap();
        }
    }
    #pragma warning restore CS1591
}
