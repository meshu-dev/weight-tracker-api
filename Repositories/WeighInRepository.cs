using System;
using System.Collections.Generic;
using System.Linq;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;

namespace WeightTracker.Api.Repositories
{
    public class WeighInRepository : BaseRepository
    {
        public WeighInRepository(DataContext context) : base(context) { }

        public WeighIn Get(Guid id)
        {
            return _context.WeighIns.Find(id);
        }

        public IEnumerable<WeighIn> GetRows()
        {
            return _context.WeighIns.ToList();
        }
    }
}
