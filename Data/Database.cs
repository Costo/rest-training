using rest_training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_training.Data
{
    public class Database
    {
        public IDictionary<int, Attendee> Attendees { get; } = new Dictionary<int, Attendee>();
        public IDictionary<int, Customer> Customers { get; } = new Dictionary<int, Customer>();
        public IDictionary<int, Order> Orders { get; } = new Dictionary<int, Order>();

    }
}
