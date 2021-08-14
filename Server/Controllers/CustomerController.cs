using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CustomerPartsTracker.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using CustomerPartsTracker.Server.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerPartsTracker.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerRepository _repository;

        public CustomerController()
        {
            _repository = new CustomerRepository(new CustomerPartsTrackerContext());
        }

        private static string NotFoundString(int id) => $"No {nameof(Customer)} with {nameof(Customer.Id)} {id} was found.";

        // GET: api/<CustomerController>
        [HttpGet]
        public List<Customer> GetCustomers() => _repository.GetItems();

        // GET api/<CustomerController>/5
        [HttpGet("{customerId}")]
        [ActionName(nameof(GetCustomer))]
        public ActionResult<Customer> GetCustomer(short customerId)
        {
            try
            {
                var returnedCustomer = _repository.GetItemByID(e => e.Id == customerId);
                return Ok(returnedCustomer);
            }
            catch(InvalidOperationException)
            {
                return NotFound(NotFoundString(customerId));
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public ActionResult<Customer> AddUpdateCustomer(Customer receivedCustomer)
        {
            try
            {
                var result = _repository.Update(receivedCustomer);
                if (receivedCustomer.Id == 0) return CreatedAtAction(nameof(GetCustomer), new { customerId = result.Id }, result);
                else return Ok(result);
            }
            catch(DbUpdateConcurrencyException)
            {
                return NotFound(NotFoundString(receivedCustomer.Id));
            }
            catch(ValidationException ex)
            {
                return ValidationProblem(ex.Message);
            }
            catch(DbUpdateException ex)
            {
                return Problem(ex.Message, "", 500, "Error.", "Error.");
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{customerId}")]
        public ActionResult DeleteCustomer(short customerId)
        {
            try
            {
                _repository.Delete(e => e.Id == customerId);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound(NotFoundString(customerId));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, "", 500, "Error.", "Error.");
            }
        }
    }
}