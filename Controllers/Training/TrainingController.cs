using Microsoft.AspNet.Mvc;
using rest_training.Data;
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
                title = "Building Web APIs",
                startDate = new DateTimeOffset(2016, 3, 2, 10, 0, 0, TimeSpan.FromHours(-5)),
                endDate = new DateTimeOffset(2016, 3, 2, 13, 0, 0, TimeSpan.FromHours(-5)),
                numberOfAttendees = _database.Attendees.Count,
                links = new[]
                {
                    new {
                    href=  Url.Link("GetAttendees", null),
                    rel = "attendees",
                }
                }
            });
        }
        
    }
}
