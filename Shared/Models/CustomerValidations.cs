using System.Collections.Generic;

namespace CustomerPartsTracker.Shared.Models
{
    public class CustomerValidations
    {
        private string Name { get; set; }
        private List<Customer> Customers { get; set; }
        public const int MinLength = 3;
        public const int MaxLength = 65;

        public CustomerValidations(string name, List<Customer> customers)
        {
            Name = name.Trim();
            Customers = customers;
        }

        public (bool, string) Validate()
        {
            if (string.IsNullOrEmpty(Name)) return (false, $"{nameof(Customer)} {nameof(Customer.Name)} required.");
            else if (Name.Length < MinLength) return (false, $"{nameof(Customer)} {nameof(Customer.Name)} too short. Must be at least {MinLength} characters long.");
            else if (Name.Length > MaxLength) return (false, $"{nameof(Customer)} {nameof(Customer.Name)} too long. Must be at most {MaxLength} characters long.");
            else if (Customers.Exists(x => x.Name == Name)) return (false, $"{nameof(Customer)} {Name} already exists.");
            return (true, "");
        }
    }
}
