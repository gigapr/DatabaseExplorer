using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using DatabaseSchemaReader.WebHost.Attributes;

namespace DatabaseSchemaReader.WebHost.Providers
{
    public class ApiDocumentationProvider : IDocumentationProvider
    {
        public string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var actionDescriptionAttributes = actionDescriptor.GetCustomAttributes<DescriptionAttribute>();

            return actionDescriptionAttributes.First().Description;                  
        }

        public string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            var parameterDescriptionAttributes = parameterDescriptor.GetCustomAttributes<ParameterDescriptionAttribute>();

            return parameterDescriptionAttributes.Any() ? parameterDescriptionAttributes.First().Description : string.Empty;
        }
    }
}