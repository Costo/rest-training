using Microsoft.AspNet.JsonPatch;
using Swashbuckle.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace rest_training.SwaggerGen
{
    public class PatchFilter : IOperationFilter, IModelFilter
    {
        public void Apply(Schema model, ModelFilterContext context)
        {
            if (context.SystemType.GetTypeInfo().IsGenericType && context.SystemType.GetGenericTypeDefinition() == typeof(JsonPatchDocument<>))
            {
                model.Example = new[]
                {
                   new {
                        op = "replace",
                        path = "/path",
                        value = "value"
                    }
                };
            }
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.HttpMethod == "PATCH")
            {
                operation.Consumes = new List<string> { "application/json-patch+json" };
            }
        }
    }
}
