using System.Data.OleDb;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using DatabaseSchemaReader.Contract.Exceptions.Interfaces;
using DatabaseSchemaReader.WebHost.Exceptions.Interfaces;

namespace DatabaseSchemaReader.WebHost.Attributes
{
    public class SchemaExplorerExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is IDatabaseTypeException)
            {
                ThrowDatabaseTypeException(context);
            }

            else if (context.Exception is ITableNotFoundException)
            {
                ThrowTableNotFoundException(context);
            }
            else if (context.Exception is OleDbException)
            {
                ThrowConnectionException(context);
            }
            else
            {
                ThrowGeneralException();
            }
        }

        private static void ThrowConnectionException(HttpActionExecutedContext context)
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(context.Exception.Message),
                ReasonPhrase = "Unable to connect to the database"
            });
        }

        private static void ThrowTableNotFoundException(HttpActionExecutedContext context)
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(context.Exception.Message),
                ReasonPhrase = "Table not found."
            });
        }

        private static void ThrowDatabaseTypeException(HttpActionExecutedContext context)
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(context.Exception.Message),
                ReasonPhrase = "The Database type is not supported"
            });
        }

        private static void ThrowGeneralException()
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An error occurred, please try again or contact the administrator."),
                ReasonPhrase = "Critical Exception"
            });
        }
    }
}