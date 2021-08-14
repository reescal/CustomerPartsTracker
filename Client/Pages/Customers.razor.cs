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
    public partial class Customers
    {
        private List<Customer> customers;
        private Customer selectedCustomer;

        private CustomSnackbar customSnackbar;

        [Inject]
        private HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync() => customers = await Http.GetFromJsonAsync<List<Customer>>(Customer.Uri);

        private void ValidateCustomer(ValidatorEventArgs e)
        {
            bool result;
            (result, e.ErrorText) = new CustomerValidations(Convert.ToString(e.Value), customers).Validate();
            e.Status = result ? ValidationStatus.Success : ValidationStatus.Error;
        }

        private async Task Post(Customer inputCustomer)
        {
            var response = await Http.PostAsJsonAsync<Customer>(Customer.Uri, inputCustomer);
            try
            {
                response.EnsureSuccessStatusCode();
                Customer returnedCustomer = await response.Content.ReadFromJsonAsync<Customer>();
                customers[customers.FindIndex(x => x.Name == returnedCustomer.Name)] = returnedCustomer;
                customSnackbar.SetColorMessageAndShow(SnackbarColor.Success, $"{nameof(Customer)} {inputCustomer.Action()} successfully.");
            }
            catch
            {
                if (inputCustomer.Id == 0) customers.RemoveAt(customers.Count - 1);
                customSnackbar.SetColorMessageAndShow(SnackbarColor.Danger, await response.Content.ReadAsStringAsync());
            }
            StateHasChanged();
        }
    }
}