using AutoMapper;
using System;
using System.Linq;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(DataContext context, IMapper mapper) : base(context, mapper) {}

        public UserModel Create(UserModel model)
        {
            var entity = _mapper.Map<User>(model);

            Create(entity);

            if (Save() == true) {
                return _mapper.Map<UserModel>(entity);
            }
            return null;
        }

        public UserModel Read(int id)
        {
            var entity = _context.Users.Find(id);

            if (entity == null) return null;

            return _mapper.Map<UserModel>(entity);
        }

        public UserModel[] ReadAll()
        {
            var entities = _context.WeighIns.ToArray();

            if (entities == null) return null;

            return _mapper.Map<UserModel[]>(entities);
        }
    }
}
