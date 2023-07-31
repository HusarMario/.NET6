namespace Web.Middleware
{
    public class TerminalMiddleware
    {

        private RequestDelegate _next;

        public TerminalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/terminal")
            {
                await context.Response.WriteAsync("Terminal");
            }

            if (_next != null)
            {
                await _next(context);
            }

        }
    }
}
