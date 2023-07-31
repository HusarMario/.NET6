namespace Web.Middleware
{
    public class ResponseMiddleware
    {
        private RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            await context.Response.WriteAsync($"Status code: {context.Response.StatusCode}.\n");
        }
    }
}
