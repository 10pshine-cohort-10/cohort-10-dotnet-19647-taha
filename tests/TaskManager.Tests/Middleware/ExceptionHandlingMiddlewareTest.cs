using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using TaskManager.Api.Middleware;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Tests.Middleware;

public class ExceptionHandlingMiddlewareTests
{
    public static TheoryData<Exception, int> ExceptionCases => new()
    {
        { new ValidationException("Invalid input"), StatusCodes.Status400BadRequest },
        { new ForbiddenException(), StatusCodes.Status403Forbidden },
        { new NotFoundException("Task", 42), StatusCodes.Status404NotFound },
        { new InvalidOperationException("boom"), StatusCodes.Status500InternalServerError },
    };

    [Theory]
    [MemberData(nameof(ExceptionCases))]
    public async Task InvokeAsync_MapsExceptionToProblemDetails(Exception exception, int expectedStatus)
    {
        var (context, body) = await InvokeWithAsync(_ => throw exception);

        Assert.Equal(expectedStatus, context.Response.StatusCode);
        Assert.StartsWith("application/problem+json", context.Response.ContentType);
        Assert.Equal(expectedStatus, body.GetProperty("status").GetInt32());
    }

    [Fact]
    public async Task InvokeAsync_ValidationException_IncludesErrors()
    {
        var errors = new Dictionary<string, string[]> { ["title"] = ["Title is required."] };

        var (_, body) = await InvokeWithAsync(_ => throw new ValidationException(errors));

        var titleErrors = body.GetProperty("errors").GetProperty("title");
        Assert.Equal("Title is required.", titleErrors[0].GetString());
    }

    [Fact]
    public async Task InvokeAsync_UnhandledException_HidesInternalMessage()
    {
        var (_, body) = await InvokeWithAsync(_ => throw new InvalidOperationException("secret details"));

        Assert.DoesNotContain("secret details", body.GetRawText());
    }

    [Fact]
    public async Task InvokeAsync_NoException_PassesThrough()
    {
        var context = new DefaultHttpContext();
        var middleware = CreateMiddleware(ctx =>
        {
            ctx.Response.StatusCode = StatusCodes.Status204NoContent;
            return Task.CompletedTask;
        });

        await middleware.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status204NoContent, context.Response.StatusCode);
    }

        [Fact]
    public async Task InvokeAsync_ClearsResponseSetBeforeException()
    {
        var (context, body) = await InvokeWithAsync(ctx =>
        {
            ctx.Response.ContentLength = 1;
            ctx.Response.Headers["X-Partial"] = "true";
            throw new InvalidOperationException("boom");
        });

        Assert.Null(context.Response.ContentLength);
        Assert.False(context.Response.Headers.ContainsKey("X-Partial"));
        Assert.Equal(StatusCodes.Status500InternalServerError, body.GetProperty("status").GetInt32());
    }

    [Fact]
    public void Constructor_NullNext_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new ExceptionHandlingMiddleware(null!, NullLogger<ExceptionHandlingMiddleware>.Instance));
    }

    private static ExceptionHandlingMiddleware CreateMiddleware(RequestDelegate next) =>
        new(next, NullLogger<ExceptionHandlingMiddleware>.Instance);

    private static async Task<(HttpContext Context, JsonElement Body)> InvokeWithAsync(RequestDelegate next)
    {
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        await CreateMiddleware(next).InvokeAsync(context);

        context.Response.Body.Position = 0;
        using var document = await JsonDocument.ParseAsync(context.Response.Body);
        return (context, document.RootElement.Clone());
    }
}