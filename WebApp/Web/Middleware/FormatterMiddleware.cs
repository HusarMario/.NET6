using Web.Services.Formatter;

namespace Web.Middleware
{
    public class FormatterMiddleware
    {
        private RequestDelegate _next;

        public FormatterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter formatter1, IResponseFormatter formatter2, IResponseFormatter formatter3)
        {
            await _next(context);
            await formatter1.Format(context, String.Empty);
            await formatter1.Format(context, String.Empty);
            await formatter1.Format(context, String.Empty);
            
        }
    }
}
