using CustomerPartsTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPartsTracker.Server.Data
{
    public class TrackerRepository
    {
        internal CustomerPartsTrackerContext context;
        internal DbSet<Tracker> dbSet;

        public TrackerRepository(CustomerPartsTrackerContext context)
        {
            this.context = context;
            dbSet = context.Set<Tracker>();
        }

        public List<Tracker> GetItems() => dbSet.AsNoTracking().Include(p => p.Part).ThenInclude(c => c.Customer).ToList();

        public Tracker GetItemByID(Func<Tracker, bool> predicate) => dbSet.Include(p => p.Part).Single(predicate);

        public List<Tracker> GetItemsWhere(Func<Tracker, bool> predicate) => dbSet.AsNoTracking().Include(p => p.Part).Where(predicate).ToList();

        public Tracker AddUpdate(Tracker entity)
        {
            if (string.IsNullOrEmpty(entity.LotNo)) entity.GenerateLot();
            var result = dbSet.Update(entity);
            Save();
            context.Entry(entity).Reference(e => e.Part).Load();
            context.Entry(entity.Part).Reference(e => e.Customer).Load();
            return LoadSubEntities(entity);
        }

        public List<Tracker> AddUpdate(List<Tracker> entities)
        {
            if (string.IsNullOrEmpty(entities[0].LotNo))  entities.ForEach(x => x.GenerateLot());
            dbSet.UpdateRange(entities);
            Save();
            foreach (var entity in entities)
            {
                LoadSubEntities(entity);
            }
            return entities;
        }

        private Tracker LoadSubEntities(Tracker entity)
        {
            context.Entry(entity).Reference(e => e.Part).Load();
            context.Entry(entity.Part).Reference(e => e.Customer).Load();
            return entity;
        }

        public void Delete(Func<Tracker, bool> predicate)
        {
            dbSet.Remove(GetItemByID(predicate));
            Save();
        }

        private void Save() => context.SaveChanges();
    }
}
