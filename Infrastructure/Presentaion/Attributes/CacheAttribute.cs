using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Presentation.Attributes
{
    internal class CacheAttribute(int DurationInSecond = 90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Create a cache key based on the request URL and parameters

            string CacheKey = CreateCacheKey(context.HttpContext.Request);

            // Return Value If Not Null

            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheValue = await cacheService.GetAsync(CacheKey);

            // Return Value If Is Null
            if(cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };

                return;
            }

            // Invoke .Next
            var ExecutedContext = await next.Invoke();

            // Set Value With Cache Key
            if(ExecutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(CacheKey, result.Value, TimeSpan.FromSeconds(DurationInSecond));
            }

        }

        private string CreateCacheKey(HttpRequest request)
        {
            // {{BaseUrl}}/api/products?brandId=4&typeId=2

            StringBuilder Key = new StringBuilder();

            Key.Append(request.Path + '?');

            foreach (var Item in request.Query.OrderBy(Q => Q.Key))
            {
                Key.Append($"{Item.Key}={Item.Value}&");
            };

            return Key.ToString();
        }
    }
}
