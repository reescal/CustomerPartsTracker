using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable enable

namespace CustomerPartsTracker.Shared.Models
{
    public partial class Part
    {
        [Range(0, short.MaxValue, ErrorMessage = "Invalid part ID.")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Part number required.")]
        [StringLength(PartValidations.NumberMaxLength, MinimumLength = PartValidations.NumberMinLength, ErrorMessage = "Part number must be between 1-25 characters long.")]
        public string Number
        {
            get => number;
            set => number = value.Trim();
        }

        [Required(ErrorMessage = "Part name required.")]
        [StringLength(PartValidations.NameMaxLength, MinimumLength = PartValidations.NameMinLength, ErrorMessage = "Part name must be between 1-65 characters long.")]
        public string Name
        {
            get => name;
            set => name = value.Trim();
        }

        [Required(ErrorMessage = "Customer ID required.")]
        [Range(1, short.MaxValue, ErrorMessage = "Invalid customer ID.")]
        public short CustomerId { get; set; }

        private string number;
        private string name;

        public const string Uri = "api/" + nameof(Part);

        public virtual Customer? Customer { get; set; }
        [JsonIgnore]
        public virtual List<Tracker>? Trackers { get; set; }

        public Part()
        {
            Id = 0;
            Number = "";
            Name = "";
            CustomerId = 0;
            Customer = null;
        }

        public Part(Part p)
        {
            Id = p.Id;
            Number = p.Number;
            Name = p.Name;
            CustomerId = p.CustomerId;
            Customer = p.Customer ?? null;
        }

        public string Action() => Id == 0 ? "added" : "updated";

    }
}
