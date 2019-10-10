using AutoMapper;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Migrations
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            this.CreateMap<User, UserModel>()
                .ReverseMap();

            this.CreateMap<WeighIn, WeighInModel>()
                .ReverseMap();
        }
    }
}
