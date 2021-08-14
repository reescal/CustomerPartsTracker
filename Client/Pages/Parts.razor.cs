using Blazorise;
using Blazorise.Snackbar;
using CustomerPartsTracker.Client.Shared;
using CustomerPartsTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CustomerPartsTracker.Client.Pages
{
    public partial class Parts
    {
        [Inject]
        private HttpClient Http { get; set; }
        private List<Part> parts;
        private List<Customer> customers;
        private Part selectedPart = new();
        private Part inputPart = new();

        private Validations validations;
        private Modal modal;
        private CustomSnackbar customSnackbar;
        private bool progressBar;
        private static (string Add, string Edit) modalAction = ("Add", "Edit");
        private string modalTitle = "";

        protected override async Task OnInitializedAsync()
        {
            progressBar = true;
            var partsTask = Http.GetFromJsonAsync<List<Part>>(Part.Uri);
            var customersTask = Http.GetFromJsonAsync<List<Customer>>(Customer.Uri);
            parts = await partsTask;
            customers = await customersTask;
            progressBar = false;
        }

        private void ShowModal(string action)
        {
            inputPart = string.Equals(action, modalAction.Add) ? new Part() : new Part(selectedPart) { Customer = null };
            modalTitle = string.Join(" ", new string[] { action, nameof(Part) });
            validations.ClearAll();
            modal.Show();
        }

        protected async Task Post()
        {
            var response = await Http.PostAsJsonAsync<Part>(Part.Uri, inputPart);
            try
            {
                response.EnsureSuccessStatusCode();
                Part receivedPart = await response.Content.ReadFromJsonAsync<Part>();
                if (receivedPart.Id != inputPart.Id) parts.Add(receivedPart);
                else parts[parts.FindIndex(x => x.Id == receivedPart.Id)] = receivedPart;
                customSnackbar.SetColorMessageAndShow(SnackbarColor.Success, $"{nameof(Part)} {inputPart.Action()} successfully.");
                StateHasChanged();
                modal.Hide();
            }
            catch (Exception ex)
            {
                customSnackbar.SetColorMessageAndShow(SnackbarColor.Danger, ex.Message);
            }
        }

        private async Task DuplicateCheck()
        {
            var result = new PartValidations(inputPart, parts).Validate();
            if (result.Item1) await Post();
            else customSnackbar.SetColorMessageAndShow(SnackbarColor.Danger, result.Item2);
        }
    }
}