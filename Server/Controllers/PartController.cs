using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CustomerPartsTracker.Server.Data;
using CustomerPartsTracker.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerPartsTracker.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private readonly PartRepository _repository;

        public PartController()
        {
            _repository = new PartRepository(new CustomerPartsTrackerContext());
        }

        private static string NotFoundString(int id) => $"No {nameof(Part)} with {nameof(Part.Id)} {id} was found.";

        // GET: api/<PartController>
        [HttpGet]
        public List<Part> GetParts() => _repository.GetItems();

        // GET api/<PartController>/customer/5
        [HttpGet("customer/{customerId}")]
        public ActionResult<List<Part>> GetCustomerParts(short customerId) => Ok(_repository.GetItemsWhere(c => c.CustomerId == customerId));

        // GET api/<PartController>/3
        [HttpGet("{partId}")]
        [ActionName(nameof(GetPart))]
        public ActionResult<Part> GetPart(short partId)
        {
            try
            {
                var result = _repository.GetItemByID(e => e.Id == partId);
                return Ok(result);
            }
            catch(InvalidOperationException)
            {
                return NotFound(NotFoundString(partId));
            }
        }

        // POST api/<PartController>/5
        [HttpPost]
        public ActionResult<Part> AddUpdatePart(Part receivedPart)
        {
            try
            {
                var returnedPart = _repository.AddUpdate(receivedPart);
                if (receivedPart.Id == 0) return CreatedAtAction(nameof(GetPart), new { partId = returnedPart.Id }, returnedPart);
                else return Ok(returnedPart);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(NotFoundString(receivedPart.Id));
            }
            catch (ValidationException ex)
            {
                return ValidationProblem(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return Problem(ex.Message, "", 500, "Error.", "Error.");
            }
        }

        // DELETE api/<PartController>/5/3
        [HttpDelete("{partId}")]
        public ActionResult DeletePart(short partId)
        {
            try
            {
                _repository.Delete(e => e.Id == partId);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound(NotFoundString(partId));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, "", 500, "Error.", "Error.");
            }
        }
    }
}