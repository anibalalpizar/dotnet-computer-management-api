using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;

namespace ComputerApi.API.Middleware
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Type = "string";
                schema.Enum.Clear();

                foreach (var enumName in Enum.GetNames(context.Type))
                {
                    var memberInfo = context.Type.GetField(enumName);
                    var descriptionAttribute = memberInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .Cast<DescriptionAttribute>()
                        .FirstOrDefault();

                    var description = descriptionAttribute?.Description ?? enumName;
                    schema.Enum.Add(new Microsoft.OpenApi.Any.OpenApiString(enumName));
                }
            }
        }
    }
}