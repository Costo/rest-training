using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using rest_training.Models;
using Microsoft.AspNet.JsonPatch;
using System.Net;
using rest_training.Data;
using rest_training.Utils;
using System.Dynamic;

namespace rest_training.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController: Controller
    {
        readonly Database _database;
        readonly IdGenerator _generator;
        public CustomersController(Database database, IdGenerator generator)
        {
            _database = database;
            _generator = generator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_database.Customers.Select(x => BuildRepresentation(x.Key, x.Value, null)));
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public IActionResult Get(int id)
        {
            if(!_database.Customers.ContainsKey(id))
            {
                return HttpNotFound();
            }
            return Ok(_database.Customers[id]);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            customer.Id = id;
            Customers[id] = customer;

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            var id = GetNextCustomerId();
            customer.Id = id;
            Customers[id] = customer;

            return CreatedAtRoute("GetCustomerById", new { id }, customer);
        }

        [HttpDelete()]
        public IActionResult Delete()
        {
            if(!Request.Query.ContainsKey("admin"))
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
            }
            Customers.Clear();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Customers.ContainsKey(id))
            {
                return HttpNotFound();
            }
            Customers.Remove(id);

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]JsonPatchDocument<Customer> patch)
        {
            if (!Customers.ContainsKey(id))
            {
                return HttpNotFound();
            }
            patch.ApplyTo(Customers[id]);

            return Ok(Customers[id]);
        }

        private dynamic BuildRepresentation(int id, Customer customer, KeyValuePair<int, Order>[] orders)
        {
            var result = (dynamic)new ExpandoObject();

            result.Name = customer.Name;
            result.Address = customer.Address;
            result.Links = new[]
            {
                new Link(Url.Link("GetCustomerById", new { id }), "self"),
            };

            if (orders != null)
            {
                result.Orders = orders.Select(x => BuildRepresentation(x.Key, x.Value)).ToArray();
            }

            return result;
        }

        private dynamic BuildRepresentation(int id, Order order)
        {
            var result = (dynamic)new ExpandoObject();

            result.Lines = order.Lines;
            result.Link = new[]
            {
                new Link(Url.Link("GetOrderById", new { id }), "self"),
            };

            return result;
        }


    }
}
