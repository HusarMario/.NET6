namespace Web.Services.Formatter
{
    public class GuidResponseFormatter : IResponseFormatter
    {
        private Guid _guid = Guid.NewGuid();

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"Guid {_guid} \n {content} \n");
        }
    }
}
