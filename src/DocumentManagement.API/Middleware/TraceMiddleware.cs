using Microsoft.AspNetCore.Http.Extensions;
using System.Diagnostics;

namespace DocumentManagement.API.Middleware
{
    public sealed class TraceMiddleware : IMiddleware
    {
        private readonly ILogger<TraceMiddleware> _logger;

        public TraceMiddleware(ILogger<TraceMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var url = context.Request.GetDisplayUrl();
            _logger.LogInformation("Request started：{method} {path}", context.Request.Method, url);

            var startTimestamp = Stopwatch.GetTimestamp();

            await next(context);

            var stopTimestamp = Stopwatch.GetTimestamp();
            var elapsed = new TimeSpan((long)(TimestampToTicks * (stopTimestamp - startTimestamp)));
            _logger.LogInformation("Request ended：{time}ms {code} {type}",
                elapsed.TotalMilliseconds,
                context.Response.StatusCode,
                context.Response.ContentType);
        }

        private static readonly double TimestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;
    }
}
