using AutoMapper;
using System;
using System.Linq;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(DataContext context, IMapper mapper) : base(context, mapper) {}

        public UserModel Read(Guid id)
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
