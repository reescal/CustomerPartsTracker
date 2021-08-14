using CustomerPartsTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CustomerPartsTracker.Server.Data
{
    public class PartRepository
    {

        internal CustomerPartsTrackerContext context;
        internal DbSet<Part> dbSet;

        public PartRepository(CustomerPartsTrackerContext context)
        {
            this.context = context;
            dbSet = context.Set<Part>();
        }

        public List<Part> GetItems() => dbSet.AsNoTracking().Include(c => c.Customer).ToList();

        public Part GetItemByID(Func<Part, bool> predicate) => dbSet.Include(c => c.Customer).Single(predicate);

        public List<Part> GetItemsWhere(Func<Part, bool> predicate) => dbSet.Include(c => c.Customer).Where(predicate).ToList();

        public Part AddUpdate(Part entity)
        {
            var validationResult = new PartValidations(entity, GetItems()).Validate();
            if (!validationResult.Item1) throw new ValidationException(validationResult.Item2);
            var result = dbSet.Update(entity);
            Save();
            context.Entry(entity).Reference(e => e.Customer).Load();
            return result.Entity;
        }

        public void Delete(Func<Part, bool> predicate)
        {
            dbSet.Remove(GetItemByID(predicate));
            Save();
        }

        private void Save() => context.SaveChanges();
    }
}