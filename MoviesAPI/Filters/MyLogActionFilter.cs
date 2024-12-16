using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesAPI.Filters;

public class MyLogActionFilter: IActionFilter {
  private readonly ILogger<MyLogActionFilter> logger;

  public MyLogActionFilter(ILogger<MyLogActionFilter> logger) {
    this.logger = logger;
  }
  public void OnActionExecuting(ActionExecutingContext context) {
    logger.LogDebug("MyLogActionFilter.OnActionExecuting");
    foreach (KeyValuePair<string, object> kvp in context.ActionArguments) {
      logger.LogDebug("OnActionExecuting ActionArguments Key: " + kvp.Key.ToString() + ", Value: " + kvp.Value);
    }
    // Log body content. Doesn't work. However, body content is in ActionArguments.
    // System.NotSupportedException: Specified method is not supported.
    // var body = context.HttpContext.Response.Body;
    // body.Seek(0, SeekOrigin.Begin);
    // var text = new StreamReader(body).ReadToEndAsync();
    // body.Seek(0, SeekOrigin.Begin);
    // logger.LogWarning($"context.HttpContext.Response.Body: {text}");
  }

  public void OnActionExecuted(ActionExecutedContext context) {
    logger.LogDebug("MyLogActionFilter.OnActionExecuted");
    logger.LogDebug($"MyLogActionFilter.OnActionExecuted context.HttpContext.Response.StatusCode = {context.HttpContext.Response.StatusCode}");
    foreach (var headers in context.HttpContext.Response.Headers) {
      logger.LogDebug($"MyLogActionFilter.OnActionExecuted context.HttpContext.Response.Headers Key: {headers.Key}, Value: {headers.Value} ");
    }
    // Log body content. Doesn't work
    // var body = context.HttpContext.Response.Body;
    // body.Seek(0, SeekOrigin.Begin);
    // var text = await new StreamReader(body).ReadToEndAsync();
    // body.Seek(0, SeekOrigin.Begin);
    // logger.LogWarning($"Response {text}");
  }
}