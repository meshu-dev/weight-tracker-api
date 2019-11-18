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

            this.CreateMap<Role, RoleModel>()
                .ReverseMap();

            this.CreateMap<User, UserModel>()
                .ReverseMap()
                .ForMember(u => u.Role, opt => opt.Ignore())
                .ForMember(u => u.Unit, opt => opt.Ignore());

            this.CreateMap<WeighIn, WeighInModel>()
                .ReverseMap()
                .ForMember(w => w.User, opt => opt.Ignore());

            this.CreateMap<WeighIn, UserWeighInModel>()
                .ReverseMap()
                .ForMember(w => w.User, opt => opt.Ignore());
        }
    }
    #pragma warning restore CS1591
}
