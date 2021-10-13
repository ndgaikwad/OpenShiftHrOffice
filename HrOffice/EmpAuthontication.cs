using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HrOffice
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class EmpAuthontication
    {
        private readonly RequestDelegate _next;

        public EmpAuthontication(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            //write preprocessing logic 
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class EmpAuthonticationExtensions
    {
        public static IApplicationBuilder UseEmpAuthontication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EmpAuthontication>();
        }
    }
}
