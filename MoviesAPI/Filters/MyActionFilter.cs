using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesAPI.Filters;

public class MyActionFilter: IActionFilter {
  private readonly ILogger<MyActionFilter> logger;

  public MyActionFilter(ILogger<MyActionFilter> logger) {
    this.logger = logger;
  }
  public void OnActionExecuting(ActionExecutingContext context) {
    logger.LogDebug("OnActionExecuting");
    foreach (KeyValuePair<string, object> kvp in context.ActionArguments) {
      logger.LogDebug("OnActionExecuting ActionArguments Key: " + kvp.Key.ToString() + ", Value: " + kvp.Value);
    }
  }

  public void OnActionExecuted(ActionExecutedContext context) {
    logger.LogDebug("OnActionExecuted");
    logger.LogDebug($"OnActionExecuted context.HttpContext.Response.StatusCode = {context.HttpContext.Response.StatusCode}");
    foreach (var headers in context.HttpContext.Response.Headers) {
      logger.LogDebug($"OnActionExecuted Response Headers Key: {headers.Key}, Value: {headers.Value} ");
    }
    // Log body content. Doesn't work
    // var body = context.HttpContext.Response.Body;
    // body.Seek(0, SeekOrigin.Begin);
    // var text = await new StreamReader(body).ReadToEndAsync();
    // body.Seek(0, SeekOrigin.Begin);
    // logger.LogWarning($"Response {text}");
  }
}