using System.Collections.Generic;

namespace CustomerPartsTracker.Shared.Models
{
    public class PartValidations
    {
        private string Name { get; set; }
        private string Number { get; set; }
        private int CustomerId { get; set; }
        private List<Part> Parts { get; set; }
        public const int NameMinLength = 1;
        public const int NumberMinLength = 1;
        public const int NameMaxLength = 65;
        public const int NumberMaxLength = 25;

        public PartValidations(Part part, List<Part> parts)
        {
            Name = part.Name.Trim();
            Number = part.Number.Trim();
            CustomerId = part.CustomerId;
            Parts = parts;
        }

        public (bool, string) Validate()
        {
            //if (string.IsNullOrEmpty(Name)) return (false, $"{nameof(Part)} {nameof(Name)} required.");
            //else if (Name.Length > NameMaxLength) return (false, $"{nameof(Part)} {nameof(Name)} too long. Must be at most {NameMaxLength} characters long.");
            //else if (string.IsNullOrEmpty(Number)) return (false, $"{nameof(Part)} {nameof(Number)} required.");
            //else if (Number.Length > NumberMaxLength) return (false, $"{nameof(Part)} {nameof(Number)} too long. Must be at most {NumberMaxLength} characters long.");
            if (Parts.Exists(x => x.Name == Name && x.Number == Number && x.CustomerId == CustomerId)) return (false, $"{nameof(Part)} already exists.");
            return (true, "");
        }
    }
}
