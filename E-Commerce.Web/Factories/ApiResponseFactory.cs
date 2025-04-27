using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorsResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(model => model.Value.Errors.Any())
                    .Select(model => new ValidationError()
                    {
                        Field = model.Key,
                        Errors = model.Value.Errors.Select(errors => errors.ErrorMessage)
                    });

            var errorResponse = new ValidationErrorToReturn()
            {
                ValidationErrors = errors,
            };

            return new BadRequestObjectResult(errorResponse);
        }
    }
}
