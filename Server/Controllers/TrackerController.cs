using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPartsTracker.Server.Data;
using CustomerPartsTracker.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerPartsTracker.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerController : ControllerBase
    {
        private readonly TrackerRepository _repository;

        public TrackerController()
        {
            _repository = new TrackerRepository(new CustomerPartsTrackerContext());
        }

        private static string NotFoundString(int id) => $"No {nameof(Tracker)} with {nameof(Tracker.Id)} {id} was found.";

        // GET: api/<TrackerController>
        [HttpGet]
        public List<Tracker> GetTrackers() => _repository.GetItems();

        // GET api/<TrackerController>/5
        [HttpGet("{trackingId}")]
        [ActionName(nameof(GetTracker))]
        public ActionResult<Tracker> GetTracker(short trackingId)
        {
            try
            {
                return Ok(_repository.GetItemByID(e => e.Id == trackingId));
            }
            catch (InvalidOperationException)
            {
                return NotFound(NotFoundString(trackingId));
            }
        }

        // GET api/<TrackerController>/customer/5
        [HttpGet("customer/{customerId}")]
        public ActionResult<List<Tracker>> GetTrackersByCustomerId(short customerId) => Ok(_repository.GetItemsWhere(t => t.Part.CustomerId == customerId));

        // GET api/<TrackerController>/part/5
        [HttpGet("part/{partId}")]
        public ActionResult<List<Tracker>> GetTrackersByPartId(short partId) => Ok(_repository.GetItemsWhere(t => t.PartId == partId));

        // POST api/<TrackerController>
        [HttpPost("batch")]
        public ActionResult<List<Tracker>> AddUpdateTrackers(List<Tracker> receivedTrackers)
        {
            try
            {
                var returnedTrackers = _repository.AddUpdate(receivedTrackers);
                return Ok(returnedTrackers);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return Problem(ex.Message, "", 500, "Error.", "Error.");
            }
        }

        // POST api/<TrackerController>
        [HttpPost]
        public ActionResult<Tracker> AddUpdateTracker(Tracker receivedTracker)
        {
            try
            {
                var result = _repository.AddUpdate(receivedTracker);
                if (receivedTracker.Id > 0) return Ok(result);
                else return CreatedAtAction(nameof(GetTracker), new { trackingId = result.Id }, result);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(NotFoundString(receivedTracker.Id));
            }
            catch (DbUpdateException ex)
            {
                return Problem(ex.Message, "", 500, "Error.", "Error.");
            }
        }

        // DELETE api/<TrackerController>/5
        [HttpDelete("{trackingId}")]
        public ActionResult<Tracker> Delete(short trackingId)
        {
            try
            {
                _repository.Delete(e => e.Id == trackingId);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound(NotFoundString(trackingId));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, "", 500, "Error.", "Error.");
            }
        }
    }
}
