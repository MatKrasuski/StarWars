using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Validation
{
    public class ValidateIdFormatAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var id = context.ActionArguments["id"].ToString();

            var isHexaDecimal = Regex.IsMatch(id, @"\A\b[0-9a-fA-F]+\b\Z");

            if (id.Length != 24 || !isHexaDecimal)
            {
                context.ModelState.AddModelError("validation error", $"Icorrect format id: {id}; Id should be 24 digit, hexadecimal string");
            }

            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
