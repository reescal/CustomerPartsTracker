using CustomerPartsTracker.Shared.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable enable

namespace CustomerPartsTracker.Shared.Models
{
    public partial class Tracker
    {
        public short Id { get; set; }

        [JsonIgnore]
        public string PT => $"{Part?.CustomerId}-{PartId}-{Id}";

        [Required(ErrorMessage = "Part ID is a mandatory field.")]
        [Range(0, short.MaxValue, ErrorMessage = "Invalid part ID.")]
        public short PartId { get; set; }

        [StringLength(TrackerValidations.SNMaxLength, ErrorMessage = "Serial number must be at most {1} characters long.")]
        public string? SerialNumber
        {
            get => _serialNumber;
            set => _serialNumber = value?.Trim();
        }

        [StringLength(TrackerValidations.SecondaryIDMaxLength, ErrorMessage = "Secondary ID must be at most {1} characters long.")]
        public string? SecondaryId
        {
            get => _secondaryId;
            set => _secondaryId = value?.Trim();
        }

        public string LotNo { get; set; }

        [StringLength(TrackerValidations.PONoMaxLength, ErrorMessage = "P.O. number must be maximum {1} characters long.")]
        public string? PoNo
        {
            get => _poNo;
            set => _poNo = value?.Trim();
        }

        [Required (ErrorMessage = "Project number is a mandatory field.")]
        [StringLength(TrackerValidations.ProjNoMaxLength, MinimumLength = TrackerValidations.ProjNoMinLength, ErrorMessage = "Project number must be between {2}-{1} characters long.")]
        public string ProjectNo
        {
            get => _projNo;
            set => _projNo = value.Trim();
        }

        [Required (ErrorMessage = "Received date is a mandatory field.")]
        [ReceivedShippedDate(ErrorMessage = "Received date must be earlier than or equal to today's date.")]
        public DateTime ReceivedDate { get; set; }

        [ReceivedShippedDate(ErrorMessage = "Shipped date must be earlier than or equal to today's date.")]
        public DateTime? ShippedDate { get; set; }

        private string? _serialNumber;
        private string? _secondaryId;
        private string? _poNo;
        private string _projNo;

        public string? QaNote { get; set; }

        public string? Comments { get; set; }

        public virtual Part? Part { get; set; }

        public static string Uri = $"api/{nameof(Tracker)}";

        public Tracker()
        {
            Id = 0;
            PartId = 0;
            SerialNumber = null;
            SecondaryId = null;
            LotNo = "";
            PoNo = null;
            ProjectNo = "";
            ReceivedDate = DateTime.Today;
            ShippedDate = null;
            QaNote = null;
            Comments = null;
        }

        public Tracker(Tracker t)
        {
            Id = t.Id;
            PartId = t.PartId;
            SerialNumber = t.SerialNumber;
            SecondaryId = t.SecondaryId;
            LotNo = t.LotNo;
            PoNo = t.PoNo;
            ProjectNo = t.ProjectNo;
            ReceivedDate = t.ReceivedDate;
            ShippedDate = t.ShippedDate;
            QaNote = t.QaNote;
            Comments = t.Comments;
            Part = t.Part ?? null;
        }

        public void GenerateLot() => LotNo = string.Join("-", new[] { PartId.ToString(), DateTime.Now.ToString("yyMMddHHmm") } );

        public string Action() => Id == 0 ? "added" : "updated";

    }
}