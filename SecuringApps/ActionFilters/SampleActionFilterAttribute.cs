using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SecuringApps.Services;
using SecuringApps.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SecuringApps.ActionFilters
{
    public class SampleActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var id = new Guid(Encryption.SymmetricDecrypt(HttpUtility.UrlDecode(context.ActionArguments["id"].ToString())));
                string currentUserId = context.HttpContext.User.Identity.Name;

                IFileServices fileServices = (IFileServices)context.HttpContext.RequestServices.GetService(typeof(IFileServices));
                //var allFiles = fileServices.GetAllFiles();
                //var fileIndex = allFiles.Where(f => f.Id == id);
                var upload = fileServices.GetFile(id);

                if(upload.UserEmail.ToString() != currentUserId && !context.HttpContext.User.IsInRole("Teacher"))
                {
                    context.Result = new UnauthorizedObjectResult("Access Denied");
                }
            }
            catch(Exception e)
            {
                context.Result = new BadRequestObjectResult("BadRequest");
            }
        }

    }
}
