using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using rest_training.Models;
using Microsoft.AspNet.JsonPatch;
using System.Net;

namespace rest_training.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController: Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new OrdersCollection
            {
                Orders = Orders.Values.ToList(),
            });
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public IActionResult Get(int id)
        {
            if (!Orders.ContainsKey(id))
            {
                return HttpNotFound();
            }
            return Ok(Orders[id]);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Order order)
        {
            order.Id = id;
            Orders[id] = order;

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            var id = GetNextOrderId();
            order.Id = id;
            Orders[id] = order;

            return CreatedAtRoute("GetOrderById", new { id }, order);
        }

        [HttpDelete()]
        public IActionResult Delete()
        {
            if (!Request.Query.ContainsKey("admin"))
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
            }
            Orders.Clear();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Orders.ContainsKey(id))
            {
                return HttpNotFound();
            }
            Orders.Remove(id);

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]JsonPatchDocument<Order> path)
        {
            if (!Orders.ContainsKey(id))
            {
                return HttpNotFound();
            }
            path.ApplyTo(Orders[id]);

            return Ok(Orders[id]);
        }

    }
}
