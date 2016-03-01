using Microsoft.AspNet.Mvc;
using rest_training.Data;
using rest_training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_training.Controllers.Training
{
    [Route("api/[controller]")]
    public class TrainingController: Controller
    {
        readonly Database _database;
        public TrainingController(Database database)
        {
            _database = database;
        }

        [HttpGet(Name = "GetTraining")]
        public IActionResult Get()
        {
            return Ok(new
            {
                Title = "Building Web APIs",
                StartDate = new DateTimeOffset(2016, 3, 2, 10, 0, 0, TimeSpan.FromHours(-5)),
                EndDate = new DateTimeOffset(2016, 3, 2, 13, 0, 0, TimeSpan.FromHours(-5)),
                NumberOfAttendees = _database.Attendees.Count,
                Links = new[]
                {
                    new Link(Url.Link("GetAttendees", null), "attendees"),
                }
            });
        }
        
    }
}
