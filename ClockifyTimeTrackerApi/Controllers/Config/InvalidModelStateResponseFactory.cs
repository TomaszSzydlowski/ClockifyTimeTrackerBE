using ClockifyTimeTrackerBE.Resources;
using ClockifyTimeTrackerBE.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ClockifyTimeTrackerBE.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(messages: errors);

            return new BadRequestObjectResult(response);
        }
    }
}
