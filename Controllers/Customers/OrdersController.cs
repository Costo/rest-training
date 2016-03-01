using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using rest_training.Models;
using Microsoft.AspNet.JsonPatch;
using System.Net;
using rest_training.Data;
using System.Dynamic;
using rest_training.Utils;

namespace rest_training.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController: Controller
    {
        readonly Database _database;
        readonly IdGenerator _generator;
        public OrdersController(Database database, IdGenerator generator)
        {
            _database = database;
            _generator = generator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_database.Orders.Select(x=> BuildRepresentation(x.Key, x.Value, null)));
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public IActionResult Get(int id)
        {
            if (!_database.Orders.ContainsKey(id))
            {
                return HttpNotFound();
            }

            var order = _database.Orders[id];
            var customer = _database.Customers.FirstOrDefault(x => x.Key == order.CustomerId).Value;

            return Ok(BuildRepresentation(id, order, customer));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Order order)
        {
            _database.Orders[id] = order;

            var customer = _database.Customers.FirstOrDefault(x => x.Key == order.CustomerId).Value;

            return Ok(BuildRepresentation(id, order, customer));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            var id = _generator.GetNextIdFor(nameof(order));

            _database.Orders[id] = order;
            var customer = _database.Customers.FirstOrDefault(x => x.Key == order.CustomerId).Value;
            

            return CreatedAtRoute("GetOrderById", new { id }, BuildRepresentation(id, order, customer));
        }

        [HttpDelete()]
        public IActionResult Delete()
        {
            if (!Request.Query.ContainsKey("admin"))
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
            }
            _database.Orders.Clear();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_database.Orders.ContainsKey(id))
            {
                return HttpNotFound();
            }
            _database.Orders.Remove(id);

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]JsonPatchDocument<Order> path)
        {
            if (!_database.Orders.ContainsKey(id))
            {
                return HttpNotFound();
            }
            var order = _database.Orders[id];
            path.ApplyTo(order);

            var customer = _database.Customers.FirstOrDefault(x => x.Key == order.CustomerId).Value;

            return Ok(BuildRepresentation(id, order, customer));
        }

        private dynamic BuildRepresentation(int id, Order order, Customer customer)
        {
            dynamic result = new ExpandoObject();

            result.Lines = order.Lines;
            result.Total = order.Lines.Sum(x => x.Quantity * x.UnitPrice);
            result.Links = new[]
            {
                new Link(Url.Link("GetOrderById", new { id }), "self"),
                new Link(Url.Link("GetCustomerById", new { id = order.CustomerId }), "customer"),
            };

            if (customer != null)
            {
                result.Customer = new
                {
                    Name = customer.Name,
                    Address = customer.Address,
                    Links = new[] {
                        new Link(Url.Link("GetCustomerById", new { id = order.CustomerId }), "self"),
                    }
                };
            }

            return result;
        }


    }
}
