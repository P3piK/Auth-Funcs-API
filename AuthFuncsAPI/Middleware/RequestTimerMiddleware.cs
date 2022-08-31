using System.Diagnostics;

namespace AuthFuncsAPI.Middleware
{
    public class RequestTimerMiddleware : IMiddleware
    {
        private readonly TimeSpan TIME_LIMIT = new TimeSpan(0, 0, 1);

        public RequestTimerMiddleware(ILogger<RequestTimerMiddleware> logger, Stopwatch stopwatch)
        {
            Logger = logger;
            Stopwatch = stopwatch;
        }

        public ILogger<RequestTimerMiddleware> Logger { get; }
        public Stopwatch Stopwatch { get; }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch.Start();
            
            await next.Invoke(context);
            
            Stopwatch.Stop();
            var elapsed = Stopwatch.Elapsed;

            if (elapsed > TIME_LIMIT)
            {
                var user = context.User?.Identity?.Name;
                var msg = $"{context.Request.Method} {context.Request.Path} took {elapsed} to complete.";

                if (user != null)
                {
                    String.Concat(msg, $" User: {user}");
                }

                Logger.LogInformation(msg);
            }
        }
    }
}
