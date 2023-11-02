


namespace ERP.Person.Middlewares
{
    public class GlobalExceptionHandlerMiddleware :IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly PersonDbContext _dbContext;
        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger, PersonDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);

            }
            catch (Exception e)
            {

                _logger.LogError(e, $"Something went wrong with message {e.Message}");
                context.Response.ContentType = "application/json+problem";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error",
                    RequestId = Activity.Current?.Id
                }.ToString());
                var logs = new ExceptionLog
                {
                    Message =
                        $"The Error statusCode is :{context.Response.StatusCode}+The Message:Internal Server Error+The RequestId is:{Activity.Current?.Id}"
                };
                _dbContext.ExceptionLogs.Add(logs);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
