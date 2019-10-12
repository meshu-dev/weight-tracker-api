using AutoMapper;
using System;
using System.Linq;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public class UserRepository : IRepository<UserModel>
    {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserModel Create(UserModel model)
        {
            var entity = _mapper.Map<User>(model);

            _context.Add(entity);

            if (Save() == true) {
                return _mapper.Map<UserModel>(entity);
            }
            return null;
        }

        public UserModel Create(IModel model)
        {
            throw new NotImplementedException();
        }

        public UserModel Delete(UserModel model)
        {
            throw new NotImplementedException();
        }

        public UserModel Delete(IModel model)
        {
            throw new NotImplementedException();
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

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
