namespace Web.Services.Formatter
{
    public interface IResponseFormatter
    {
        Task Format(HttpContext context, string content);
    }
}
