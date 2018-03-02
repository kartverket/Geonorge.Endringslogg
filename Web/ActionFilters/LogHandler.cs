using Geonorge.Endringslogg.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Endringslogg.Web.ActionFilters
{
    public class LogHandler : ActionFilterAttribute
    {
        private readonly IApplicationService _applicationService;

        public LogHandler(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("apikey", out var apiKey);

            if (string.IsNullOrEmpty(apiKey))
                context.Result = new UnauthorizedResult();

            var application = _applicationService.GetApplicationFromApiKey(apiKey).Result;

            if (string.IsNullOrEmpty(application))
                context.Result = new UnauthorizedResult();

            context.RouteData.Values.Add("application", application);
        }
    }
}
