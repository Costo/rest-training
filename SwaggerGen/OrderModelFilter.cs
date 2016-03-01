using rest_training.Models;
using Swashbuckle.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_training.SwaggerGen
{
    public class OrderModelFilter : IModelFilter
    {
        public void Apply(Schema model, ModelFilterContext context)
        {
            if (context.SystemType == typeof(Order))
            {
                model.Example = new
                {
                    CustomerId = 1,
                    Lines = new List<OrderLine>
                    {
                        new OrderLine
                        {
                            Description = "Amandes au chocolat",
                            Quantity = 10,
                            UnitPrice = 19.50m,
                        }
                    }

                };
            }
        }
    }
}
