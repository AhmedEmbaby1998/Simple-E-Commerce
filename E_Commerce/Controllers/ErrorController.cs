using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public ActionResult Error(int statusCode)
        {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                {
                   _logger.LogWarning(  $"the error comes from {feature.OriginalPath}4" +
                                      $",the query String is {feature.OriginalQueryString}");
                   return View("ErrorHandling");
                }
            }

            return View("ErrorHandling");
        }
        [Route("Exception")]
        [AllowAnonymous]
        public IActionResult Exception()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"the path of exception is {exception.Path} " +
                             $",and the Error Message is {exception.Error.Message} ," +
                             $",thr stack trace is {exception.Error.StackTrace}");
           
            return View();
        }
    

}
}