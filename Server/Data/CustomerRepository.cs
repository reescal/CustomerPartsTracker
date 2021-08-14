using CustomerPartsTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CustomerPartsTracker.Server.Data
{
    public class CustomerRepository
    {

        internal CustomerPartsTrackerContext context;
        internal DbSet<Customer> dbSet;

        public CustomerRepository(CustomerPartsTrackerContext context)
        {
            this.context = context;
            dbSet = context.Set<Customer>();
        }

        public List<Customer> GetItems() => dbSet.AsNoTracking().ToList();

        public Customer GetItemByID(Func<Customer, bool> predicate) => dbSet.Single(predicate);

        public Customer Update(Customer entity)
        {
            var validationResult = new CustomerValidations(entity.Name, GetItems()).Validate();
            if (!validationResult.Item1) throw new ValidationException(validationResult.Item2);
            var result = dbSet.Update(entity);
            Save();
            return result.Entity;
        }

        public void Delete(Func<Customer, bool> predicate)
        {
            dbSet.Remove(GetItemByID(predicate));
            Save();
        }

        private void Save() => context.SaveChanges();
    }
}