using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Core.Middlewares;

/// <summary>
/// Middleware for logging the performance of requests.
/// </summary>
public class PerformanceLoggingMiddleware
{
    private static readonly ConcurrentDictionary<string, (long totalTime, int requestCount)> RouteStats = new();
    private readonly ILogger<PerformanceLoggingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceLoggingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger.</param>
    public PerformanceLoggingMiddleware(RequestDelegate next, ILogger<PerformanceLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await next(context);
        stopwatch.Stop();
        var route = context.Request.Path.Value;

        RouteStats.AddOrUpdate(route!, (stopwatch.ElapsedMilliseconds, 1), (_, data) =>
        {
            var (totalTime, requestCount) = data;
            return (totalTime + stopwatch.ElapsedMilliseconds, requestCount + 1);
        });

        if (context.Response.StatusCode == 200)
        {
            var (averageTime, requestCount) = RouteStats[route!];
            var logData =
                $"Route: {route}, Time: {stopwatch.ElapsedMilliseconds}ms Average Time: " +
                $"{averageTime / requestCount}ms, Total Requests: {requestCount}";
            Log.Information(logData);
        }
    }
}