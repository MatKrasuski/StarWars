using System.Collections.Generic;
using API.Controllers;
using API.Services;
using API.Validation;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;

namespace UnitTests.Attributes
{
    [TestFixture]
    class ValidateIdFormatAttributeTests
    {
        [TestCase("123")]
        [TestCase("abc")]
        [TestCase("@#$%")]
        [TestCase("dasdadasdasdasdasdasdcdc")]
        [TestCase("123745348237234428734233")]
        [TestCase("234jfvf342384chfbdf233cd")]
        [TestCase("234$%(#Asdnajsd5345&$sk23")]
        public void should_return_bad_request_when_id_is_in_wrong_format(string value)
        {
            var actionArgument = "id";
            var characterServiceMock =  new Mock<ICharacterService>();

            //given
            var httpContext = new DefaultHttpContext();
            var context = new ActionExecutingContext(
                new ActionContext
                {
                    HttpContext = httpContext,
                    RouteData = new RouteData(),
                    ActionDescriptor = new ActionDescriptor(),
                },
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<CharactersController>(characterServiceMock.Object).Object);

            context.ActionArguments.Add(actionArgument, value);

            var attribute = new ValidateIdFormatAttribute();

            //when
            attribute.OnActionExecuting(context);

            //then
            context.Result.Should().NotBeNull()
                .And.BeOfType<BadRequestObjectResult>();
        }
    }
}
