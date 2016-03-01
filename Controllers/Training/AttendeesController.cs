using Microsoft.AspNet.JsonPatch;
using Microsoft.AspNet.Mvc;
using rest_training.Data;
using rest_training.Models;
using rest_training.Utils;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_training.Controllers.Training
{
    [Route("api/training/[controller]")]
    public class AttendeesController : Controller
    {
        readonly Database _database;
        readonly IdGenerator _generator;
        public AttendeesController(Database database, IdGenerator generator)
        {
            _database = database;
            _generator = generator;
        }

        [HttpGet(Name = "GetAttendees")]
        public IActionResult Get()
        {

            return Ok(_database.Attendees.Select(x => BuildRepresentation(x.Key, x.Value)));
        }

        [HttpGet("{id}", Name = "GetAttendeeById")]
        public IActionResult Get(int id)
        {
            if (!_database.Attendees.ContainsKey(id))
            {
                return HttpNotFound();
            }

            return Ok(BuildRepresentation(id, _database.Attendees[id]));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Attendee attendee)
        {
            var id = _generator.GetNextIdFor(nameof(attendee));
            attendee.RegistrationDate = DateTimeOffset.UtcNow;

            _database.Attendees.Add(id, attendee);

            return CreatedAtRoute("GetAttendeeById", new { id }, BuildRepresentation(id, attendee));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Attendee attendee)
        {
            if (!_database.Attendees.ContainsKey(id))
            {
                return HttpNotFound();
            }

            _database.Attendees[id] = attendee;

            return Ok(BuildRepresentation(id, attendee));
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]JsonPatchDocument<Attendee> patch)
        {
            if (!_database.Attendees.ContainsKey(id))
            {
                return HttpNotFound();
            }

            var attendee = _database.Attendees[id];
            patch.ApplyTo(attendee);

            return Ok(BuildRepresentation(id, attendee));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_database.Attendees.ContainsKey(id))
            {
                return HttpNotFound();
            }

            _database.Attendees.Remove(id);

            return Ok();
        }

        public dynamic BuildRepresentation(int id, Attendee data)
        {
            var result = (dynamic)new ExpandoObject();
            result.Name = data.Name;
            result.RegistrationDate = data.RegistrationDate;
            result.Links = new[]
            {
                new Link(Url.Link("GetAttendeeById", new { id }), "self"),
            };
            return result;
        }

    }
}
