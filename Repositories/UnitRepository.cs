using AutoMapper;
using System;
using System.Linq;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public class UnitRepository : BaseRepository
    {
        public UnitRepository(DataContext context, IMapper mapper) : base(context, mapper) { }

        public UnitModel Create(UnitModel model)
        {
            var entity = _mapper.Map<Unit>(model);

            Create(entity);

            if (Save() == true)
            {
                return _mapper.Map<UnitModel>(entity);
            }
            return null;
        }

        public UnitModel Read(Guid id)
        {
            var entity = _context.Units.Find(id);

            if (entity == null) return null;

            return _mapper.Map<UnitModel>(entity);
        }

        public UnitModel[] ReadAll()
        {
            var entities = _context.Units.ToArray();

            if (entities == null) return null;

            return _mapper.Map<UnitModel[]>(entities);
        }
    }
}
