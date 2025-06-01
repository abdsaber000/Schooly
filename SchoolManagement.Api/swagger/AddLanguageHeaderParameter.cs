using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SchoolManagement.Api.swagger;

public class AddLanguageHeaderParameter :  IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        // Only add if not already added
        if (!operation.Parameters.Any(p => p.Name == "Accept-Language"))
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Enum = new List<IOpenApiAny>
                    {
                        new OpenApiString("en-US"),
                        new OpenApiString("ar-EG")
                    }
                },
                Description = "Optional language header (en, ar)"
            });
        }
    }
}