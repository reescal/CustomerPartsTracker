using Blazorise.Snackbar;
using CustomerPartsTracker.Client.Shared;
using CustomerPartsTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CustomerPartsTracker.Client.Pages.Trackers
{
    public partial class Trackers
    {
        private List<Tracker> trackers;
        private List<Customer> customers;
        private List<Part> parts;
        private Tracker selectedTracker = new();
        private bool progressBar = false;
        private CustomSnackbar customSnackbar;
        private ListDownload listDownload;
        private TrackersModal modal;
        private static (string Add, string Edit) Action = ("Add", "Edit");
        private static (string SinglePart, string EntireLot) ShipOutAction = ("Single Part", "Entire Lot");
        [Inject]
        private HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            progressBar = true;
            var trackersTask = Http.GetFromJsonAsync<List<Tracker>>(Tracker.Uri);
            var partsTask = Http.GetFromJsonAsync<List<Part>>(Part.Uri);
            var customersTask = Http.GetFromJsonAsync<List<Customer>>(Customer.Uri);
            trackers = await trackersTask;
            parts = await partsTask;
            customers = await customersTask;
            progressBar = false;
        }

        private async Task ShipOut(object value)
        {
            List<Tracker> trackersToBeSent = new();
            if (value.ToString() == ShipOutAction.SinglePart) trackersToBeSent.Add(new Tracker(selectedTracker));
            else trackersToBeSent.AddRange(trackers.Where(x => x.LotNo == selectedTracker.LotNo && !x.ShippedDate.HasValue).Select(t => new Tracker(t)));
            trackersToBeSent.ForEach(x => { x.ShippedDate = DateTime.Today; x.Part = null; });
            await AddUpdateTrackers(trackersToBeSent);
        }

        private static string Uri(List<Tracker> trackersToBeSent) => trackersToBeSent.Count == 1 ? Tracker.Uri : Tracker.Uri + "/batch";

        private async Task CreateTrackers()
        {
            List<Tracker> trackersToBeSent = new();
            for (int i = 1; i <= modal.Lot.Quantity; i++)
            {
                Tracker tempTracker = new(modal.Tracker) { Part = null };
                if (modal.Lot.SerialNumbers is not null) tempTracker.SerialNumber = modal.Lot.SerialNumbers.ElementAt(i - 1);
                trackersToBeSent.Add(tempTracker);
            }
            await AddUpdateTrackers(trackersToBeSent);
        }

        private async Task AddUpdateTrackers(List<Tracker> trackersToBeSent)
        {
            var response = trackersToBeSent?.Count == 1 ? await Http.PostAsJsonAsync(Uri(trackersToBeSent), trackersToBeSent[0]) : await Http.PostAsJsonAsync(Uri(trackersToBeSent), trackersToBeSent);
            try
            {
                response.EnsureSuccessStatusCode();
                List<Tracker> receivedTrackers = new();
                if (trackersToBeSent?.Count == 1) receivedTrackers.Add(await response.Content.ReadFromJsonAsync<Tracker>());
                else receivedTrackers = await response.Content.ReadFromJsonAsync<List<Tracker>>();

                if (trackersToBeSent.All(x => x.Id == 0)) trackers.AddRange(receivedTrackers);
                else
                {
                    foreach (var tracker in receivedTrackers)
                    {
                        trackers[trackers.FindIndex(x => x.Id == tracker.Id)] = new Tracker(tracker);
                    }
                }
                customSnackbar.SetColorMessageAndShow(SnackbarColor.Success, trackersToBeSent?.Count == 1 ? $"{nameof(Tracker)} {trackersToBeSent[0].Action()} successfully." : $"{receivedTrackers?.Count}/{trackersToBeSent?.Count} trackers {trackersToBeSent[0].Action()} successfully.");
                modal.Modal.Hide();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                customSnackbar.SetColorMessageAndShow(SnackbarColor.Danger, ex.Message);
            }
        }
    }
}