using Blazorise;
using CustomerPartsTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerPartsTracker.Client.Pages.Trackers
{
    public partial class TrackersModal
    {
        public Modal Modal;
        public Tracker Tracker { get; set; } = new Tracker() { Part = new Part() };
        public (IEnumerable<string> SerialNumbers, int? Quantity) Lot;
        [Parameter]
        public (string Add, string Edit) Action { get; set; }
        [Parameter]
        public List<Customer> Customers { get; init; }
        [Parameter]
        public List<Part> Parts { get; init; }
        [Parameter]
        public EventCallback Save { get; set; }

        private bool qtyReadOnly = false;
        private string modalTitle = "";
        private Validations validations;
        private class Tag
        {
            public string Value { get; set; }
            public string Label { get; set; }
        }

        public void Show(string action, Tracker selectedTracker = null )
        {
            Lot.SerialNumbers = Lot.SerialNumbers?.Take(0);
            Lot.Quantity = action == Action.Edit ? 1 : 0;
            Tracker = action == Action.Edit ? new Tracker(selectedTracker) : new Tracker() { Part = new Part() };
            modalTitle = $"{action} {nameof(Tracker)}"; //(s) for add at end
            validations.ClearAll();
            Modal.Show();
        }

        private void PartSearchHandler(string s) => Tracker.PartId = !string.IsNullOrEmpty(s) ? default : Tracker.PartId;

        private void PartSelectionHandler(short newValue) => Tracker.PartId = newValue;

        private void CustomerSearchHandler(string s) => Tracker.Part.CustomerId = !string.IsNullOrEmpty(s) ? default : Tracker.Part.CustomerId;

        private void CustomerSelectionHandler(short newValue)
        {
            Tracker.Part.CustomerId = newValue;
            Tracker.PartId = 0;
        }

        private void OnSelectedItemsChangedHandler(IEnumerable<Tag> values) 
        {
            Lot.Quantity = values?.Count() > 0 ? values.Count() : 0;
            qtyReadOnly = values?.Count() > 0;
        }

        private void OnClearSelectedHandler()
        {
            Lot.Quantity = 0;
            qtyReadOnly = false;
        }

        private static (ValidationStatus, string) BoolToValStatus((bool result, string message)r) => (r.result ? ValidationStatus.Success : ValidationStatus.Error, r.message);

        private static void ValidateAutoComplete(ValidatorEventArgs e) => (e.Status, e.ErrorText) = Convert.ToInt16(e.Value) > 0 ? (ValidationStatus.Success, "") : (ValidationStatus.Error, "Please select from the autocomplete field.");

        private void ValidateSerialNumbers(ValidatorEventArgs e)
        {
            if (Lot.Quantity >= 1 && Lot.Quantity <= 10)
            {
                if (Lot.SerialNumbers is not null && Lot.SerialNumbers.Any(x => x.Length > TrackerValidations.SNMaxLength)) (e.Status, e.ErrorText) = (ValidationStatus.Error, $"At least one serial no. is longer than the max. {TrackerValidations.SNMaxLength} characters.");
                else e.Status = ValidationStatus.Success; //here could do Lot.Quantity <= 10
            }
            else if (Lot.Quantity == 0) (e.Status, e.ErrorText) = (ValidationStatus.Error, "Quantity must be at least one.");
            else (e.Status, e.ErrorText) = (ValidationStatus.Error, "Max. quantity or serial numbers is 10.");
        }
    }
}