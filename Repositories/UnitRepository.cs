﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    #pragma warning disable CS1591
    public class UnitRepository : Repository<UnitModel>
    {
        public UnitRepository(DataContext context, IMapper mapper) : base(context, mapper) { }

        public override UnitModel Create(UnitModel model)
        {
            var entity = mapper.Map<Unit>(model);

            context.Add(entity);

            if (Save() == true)
            {
                return mapper.Map<UnitModel>(entity);
            }
            return null;
        }

        public override UnitModel Read(int id)
        {
            var entity = context.Units.Find(id);

            if (entity == null) return null;

            context.Entry(entity).State = EntityState.Detached;
            return mapper.Map<UnitModel>(entity);
        }

        public override UnitModel[] ReadAll()
        {
            var entities = context.Units.AsNoTracking().ToArray();

            if (entities == null) return null;

            return mapper.Map<UnitModel[]>(entities);
        }

        public override UnitModel Update(UnitModel model)
        {
            var entity = context.Units.Find(model.Id);
            if (entity == null) return null;

            mapper.Map(model, entity);

            if (Save() == true)
            {
                return mapper.Map<UnitModel>(entity);
            }
            return null;
        }

        public override bool Delete(UnitModel model)
        {
            var entity = mapper.Map<Unit>(model);

            context.Units.Remove(entity);
            return Save();
        }
    }
    #pragma warning restore CS1591
}
