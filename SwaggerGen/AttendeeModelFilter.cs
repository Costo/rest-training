using rest_training.Data;
using Swashbuckle.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_training.SwaggerGen
{
    public class AttendeeModelFilter : IModelFilter
    {
        public void Apply(Schema model, ModelFilterContext context)
        {
            if (context.SystemType == typeof(Attendee))
            {
                model.Example = new
                {
                    Name = "Your Name",
                };
            }
        }
    }
}
