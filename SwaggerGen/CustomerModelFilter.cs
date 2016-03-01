using rest_training.Models;
using Swashbuckle.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_training.SwaggerGen
{
    public class CustomerModelFilter : IModelFilter
    {
        public void Apply(Schema model, ModelFilterContext context)
        {
            if (context.SystemType == typeof(Customer))
            {
                model.Example = new
                {
                    Name = "Apcurium",
                    Address = "7250 Mile End"
                };
            }
        }
    }
}
