using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;
using Swashbuckle.Swagger;

namespace WebApplication1.DLLHelp
{
    public class AddAuthorizationHeader : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation == null) return;

            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();

            }


            var parameter = new Parameter
            {
                description = "Token",
                @in = "header",
                name = "Authorization",
                required = true,
                type = "string"
            };

            if (apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                //如果Api方法是允许匿名方法，Token不是必填的

                parameter.required = false;
            }

            operation.parameters.Add(parameter);
        }
    }
}