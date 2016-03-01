using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_training.Data
{
    public class Order
    {
        public int CustomerId { get; set; }

        public List<OrderLine> Lines { get; set; } = new List<OrderLine>();

    }

    public class OrderLine
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
    
}
