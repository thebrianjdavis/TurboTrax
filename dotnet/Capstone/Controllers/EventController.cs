using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Security;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IUserDAO userDAO;
        private readonly IEventDAO eventDAO;

        public EventController(IUserDAO _userDAO, IEventDAO _eventDAO)
        {
            userDAO = _userDAO;
            eventDAO = _eventDAO;
        }

        [Authorize]
        [HttpPost("/create-event")]
        public ActionResult CreateEvent(Event newEvent)
        {

            if (newEvent != null)
            {
                newEvent.DjUserId = int.Parse(User.FindFirst("sub")?.Value);
                eventDAO.addEvent(newEvent);
                return Created("created", newEvent);
            }
            else
            {
                return BadRequest("Incomplete or missing event data");
            }
        }

        [HttpGet("/events")]
        public ActionResult<List<Event>> GetEvents()
        {
            List<Event> allEvents = eventDAO.getEvents();
            if (allEvents != null)
            {
                return Ok(allEvents);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("/users")]
        public ActionResult<List<ReturnUser>> GetAllUsers()
        {

            List<ReturnUser> allUsers = userDAO.ReturnUserList();
            if (allUsers != null)
            {
                return Ok(allUsers);
            }
            else
            {
                return BadRequest("User list is empty");
            }

        }

        //[Authorize]
        [HttpPut("/update-event/{id}")]
        public ActionResult<Event> UpdateEvent(int id, Event updatedEvent)
        {
            if (updatedEvent != null)
            {
                eventDAO.updateEvent(id, updatedEvent);
                return Created("created", updatedEvent);
            }
            else
            {
                return BadRequest("Incomplete or missing event data");
            }
        }
    
        [HttpGet("/song-shoutouts/{eventId}")]
        public ActionResult<List<SongShoutOut>> ListShoutouts(int eventId)
        {

            List<SongShoutOut> allShoutouts = eventDAO.getShoutouts(eventId);

            if (allShoutouts != null)
            {
                return Ok(allShoutouts);
            }
            else
            {
                return BadRequest("Shoutout list is empty");
            }

        }
    }
}
