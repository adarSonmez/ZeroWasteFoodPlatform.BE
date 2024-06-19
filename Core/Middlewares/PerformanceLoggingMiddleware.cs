using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Core.Middlewares;

public class PerformanceLoggingMiddleware(RequestDelegate next, ILogger<PerformanceLoggingMiddleware> logger)
{
    private static readonly ConcurrentDictionary<string, (long totalTime, int requestCount)> RouteStats = new();
    private readonly ILogger<PerformanceLoggingMiddleware> _logger = logger;

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