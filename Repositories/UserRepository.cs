using System;
using System.Collections.Generic;
using System.Linq;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;

namespace WeightTracker.Api.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(DataContext context) : base(context) {}

        public User Get(Guid id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetRows()
        {
            return _context.Users.ToList();
        }
    }
}
