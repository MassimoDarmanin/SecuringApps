using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SecuringApps.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SecuringApps.ActionFilters
{
    public class DeadlineFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var id = new Guid(HttpUtility.UrlDecode(context.ActionArguments["id"].ToString()));

                ITaskServices taskServices = (ITaskServices)context.HttpContext.RequestServices.GetService(typeof(ITaskServices));

                var upload = taskServices.GetFile(id);

                if (upload.Deadline < DateTime.Now)
                {
                    context.Result = new UnauthorizedObjectResult("Access Denied");
                }
            }
            catch (Exception e)
            {
                context.Result = new BadRequestObjectResult("BadRequest");
            }
        }
    }
}
