using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI.APIBehavoir;

/**
 * Return a List of all the errors, rather than just the first one, giving the user better feedback.
 */
public class BadRequestBehavoir {
  public static void Parse(ApiBehaviorOptions options) {
    options.InvalidModelStateResponseFactory = context => {
      var response = new List<string>();
      foreach (var modelStateEntry in context.ModelState.Values) {
        foreach (var error in modelStateEntry.Errors) {
          response.Add($"{modelStateEntry}: {error.ErrorMessage}");
        }
      }

      // Not working in my version of .NET
      // foreach (var key in context.ModelState.Keys) {
      //   foreach (var error in context.ModelState[key].Errors) {
      //     response.Add($"{key}: {error.ErrorMessage}");
      //   }
      // }
      return new BadRequestObjectResult(response);
    };
  }
}