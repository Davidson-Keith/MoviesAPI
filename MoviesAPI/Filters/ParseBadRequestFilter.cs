using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace MoviesAPI.Filters;

/**
 * A Filter to filter the execution of an Action in the case of a 400 Bad Request.
 * An Action being a call to Get/Post etc in our Controller, called by the front end.
 * 
 * This class is added by Startup as a controller filter service.
 *
 * This class implements IActionFilter, thus will process OnActionExecuted/OnActionExecuting, and ASP.NET will give us
 * an ActionExecutedContext object as a parameter to the method.
 * The ActionExecutedContext contains:
 * - Result (IActionResult) - Defines a contract that represents the result of an action method.
 * - ModelState (ModelStateDictionary) - Represents the state of an attempt to bind values from an HTTP Request to
 * an action method, which includes validation information.
 * - Many other things, e.g. HttpContext, Controller, etc.
 *
 * We then check the StatusCode of the Result, and if it is a 400 Bad Request, we then create our
 * response to return to the front end.
 */
public class ParseBadRequestFilter : IActionFilter {
  // private ILogger<Startup> logger = ApplicationDbContext.; // How do I use logging ???
  
  public void OnActionExecuting(ActionExecutingContext context) {
  }

  /**
   * Return a List of all the errors rather than just the first one, giving the front end all the errors at once.
   */
  public void OnActionExecuted(ActionExecutedContext context) {
    // context.Result is an IActionResult, and might be a BadRequestObjectResult if it is a 400 Bad Request.
    // Inheritance: Object -> ActionResult -> ObjectResult -> BadRequestObjectResult
    // ActionResult implements IActionResult
    // ObjectResult implements IActionResult, IStatusCodeActionResult
    // IStatusCodeActionResult implements IActionResult
    // Thus context.Result is at at a base level: IActionResult
    // and also all of (at least):
    //   IActionResult, IStatusCodeActionResult
    //   ActionResult, ObjectResult, BadRequestObjectResult (maybe only if it is a 400?)
    var statusCodeActionResult = context.Result as IStatusCodeActionResult;
    if (statusCodeActionResult == null) {
      return;
    }
    
    var statusCode = statusCodeActionResult.StatusCode;
    if (statusCode == 400) {
      // Initialise the response that will be a List of the error messages to send back to the front end.
      var response = new List<string>(); 
      
      // Now that we know that the ActionResult is a 400 Bad Request, then we know we can cast context Result to BadRequestObjectResult
      var badRequestObjectResult = context.Result as BadRequestObjectResult;
      if (badRequestObjectResult.Value is string) { // Does this ever happen???
        response.Add($"badRequestObjectResult.Value is string!!! Value: {badRequestObjectResult.Value}");
      }
      else {
        foreach (var modelStateEntry in context.ModelState.Values) {
          foreach (var error in modelStateEntry.Errors) {
            response.Add($"{modelStateEntry}: {error.ErrorMessage}");
          }
        }

        // Not working in my version of .NET
        // foreach (var key in context.ModelState) {
        //   foreach (var error in context.ModelState[key].Errors) {
        //     response.Add($"{key}: {error.ErrorMessage}"); 
        //   }
        // }

        // https://stackoverflow.com/questions/50162081/in-mvc-during-post-how-do-i-loop-through-a-list-of-model-objects-in-the-models
        // foreach (var key in context.ModelState.Keys)
        // {
        //   var modelStateVal    = context.ModelState[key];
        //   // var currentKeyValue  = ModelState[key].Value.AttemptedValue;
        //
        //   foreach (var error in modelStateVal.Errors)
        //   {
        //     Utility.Log(key + ": " + key);
        //     Utility.Log(error.Exception);
        //   }
        // }
      }

      context.Result = new BadRequestObjectResult(response);
    }

  }
}