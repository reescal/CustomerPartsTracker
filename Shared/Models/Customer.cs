using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace CustomerPartsTracker.Shared.Models
{
    public partial class Customer
    {
        [Range(0, short.MaxValue, ErrorMessage = "Invalid customer ID.")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Customer name required.")]
        [StringLength(CustomerValidations.MaxLength, MinimumLength = CustomerValidations.MinLength, ErrorMessage = "Customer name must be between 3-65 characters long.")]
        public string Name
        {
            get => _name;
            set => _name = value.Trim();
        }

        private string _name;

        public const string Uri = "api/" + nameof(Customer);

        [JsonIgnore]
        public virtual List<Part> Parts { get; set; }

        public Customer()
        {
            Id = 0;
            Name = "";
        }

        public Customer(Customer c)
        {
            Id = c.Id;
            Name = c.Name;
        }

        public string Action() => Id == 0 ? "added" : "updated";
    }
}