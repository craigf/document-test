using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Tests.Unit.Helpers
{
    public abstract class ControllerTestBase
    {
        protected void Is<T>(IActionResult result, HttpStatusCode statusCode) where T : OkObjectResult
        {
            var r = result as T;
            r.Should().NotBeNull();
            r?.StatusCode.Should().Be((int)statusCode);
        }

        protected TPayload Is<TResult, TPayload>(IActionResult result) where TResult : OkObjectResult
        {
            var okResult = result as TResult;
            okResult.Should().NotBeNull();
            okResult?.Value.Should().BeAssignableTo<TPayload>();
            return (TPayload)okResult?.Value;
        }
    }
}
