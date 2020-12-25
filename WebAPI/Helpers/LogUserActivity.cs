using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Extensions;
using WebAPI.Interfaces;

namespace WebAPI.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();
            var unitOfWork = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            var user = await unitOfWork.UserRepository.GetUserById(userId);
            user.LastActive = DateTime.UtcNow;
            await unitOfWork.Complete();
        }
    }
}