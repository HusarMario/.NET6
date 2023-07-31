namespace Web.Middleware
{
    public class ShortMiddleware
    {
        private RequestDelegate _next;

        public ShortMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/short")
            {
                await context.Response.WriteAsync("Request short-circuited");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
