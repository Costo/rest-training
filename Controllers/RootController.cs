using Microsoft.AspNet.Mvc;
using rest_training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_training.Controllers
{
    [Route("api")]
    public class RootController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Title = "Welcome to this wonderful API. To register for the training, go to the training registration endpoint.",
                Links = new[] {
                  new Link(Url.Link("GetTraining", null), "training")
                },
            });
        }
    }
}
