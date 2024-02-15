using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace MoviesAPI.Filters;

/**
 * Return a List of all the errors, rather than just the first one, giving the user better feedback.
 */
public class ParseBadRequestFilter : IActionFilter {
  public void OnActionExecuting(ActionExecutingContext context) {
    var result = context.Result as IStatusCodeActionResult;
    if (result == null) {
      return;
    }
    var statusCode = result.StatusCode;
    if (statusCode == 400) {
      var response = new List<string>();
      var badRequestObjectResult = context.Result as BadRequestObjectResult;
      if (badRequestObjectResult.Value is string) {
        response.Add(badRequestObjectResult.Value.ToString());
      } else {
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

  public void OnActionExecuted(ActionExecutedContext context) {
    throw new NotImplementedException();
  }
}