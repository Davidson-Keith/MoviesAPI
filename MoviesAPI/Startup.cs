using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.APIBehavior;
using MoviesAPI.Filters;

namespace MoviesAPI {
  public class Startup {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.AddDbContext<ApplicationDbContext>(options => {
        options
          .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); // From appsettings.*.json
          // .EnableSensitiveDataLogging() // Disable for production
          // .LogTo(
          //   Console.WriteLine,
          //   new[] { DbLoggerCategory.Database.Command.Name },
          //   LogLevel.Information);
      });

      services.AddControllers(options => {
        options.Filters.Add(typeof(MyExceptionFilter));
        options.Filters.Add(typeof(ParseBadRequestFilter));
      }).ConfigureApiBehaviorOptions(BadRequestBehavior.Parse);
      
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

      // Remove in final code - part of: 113 Filters
      // services.AddResponseCaching();

      // Remove in final code - part of: 114 Custom Filters
      services.AddTransient<MyLogActionFilter>();

      // services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MoviesAPI", Version = "v1" }); });

      services.AddCors(options => {
        var frontendUrl = Configuration.GetValue<string>("frontend_url");
        options.AddDefaultPolicy(builder => {
          builder.WithOrigins(frontendUrl ?? throw new InvalidOperationException("frontendUrl for CORS is null."))
            .AllowAnyMethod().AllowAnyHeader();
        });
      });

      services.AddAutoMapper(typeof(Startup));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    // NB: This is only called once for configuration, it isn't called each time a request is made.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) {
      if (env.IsDevelopment()) {
        // app.UseDeveloperExceptionPage();
        // app.UseSwagger();
        // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoviesAPI v1"));
      }

      // Demonstration of use of app.Run(...)
      // Short circuits the remainder of the pipeline.
      // app.Run( async context => {
      //   logger.LogDebug("Begin short circuit"); // Gets here
      //   await context.Response.WriteAsync("Short circuiting....");
      //   logger.LogDebug("Finish short circuit"); // Gets here
      // });
      // logger.LogDebug("Finished short circuit"); // Never gets here, pipeline is short circuited.

      // Demonstration of use of app.Map(...)
      // Only short circuits when the given path "/api/genres/sc" is requested.
      // app.Map("/api/genres/sc",
      //   (app) => {
      //     app.Run(async context => {
      //       await context.Response.WriteAsync("Short circuiting....");
      //     }); 
      //   });

      // Demonstration of:
      //    app.Use(...)
      //    await next.Invoke()
      //    context.Response.Body
      // Output comments are for http get request for: https://localhost:5234/api/genres/2
      // app.Use(async (context, next) => {
      //   using (var swapStream = new MemoryStream()) {
      //     logger.LogDebug($"swapStream.Length = {swapStream.Length}"); // Length = 0.
      //     logger.LogDebug($"context.Response.Body = {context.Response.Body}"); // Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpResponseStream
      //     var originalResponseBody = context.Response.Body; // Copy Body to originalResponseBody. Body is a stream that can't be read, but can be copied. 
      //     context.Response.Body = swapStream; // Body now points to the new empty stream (Length = 0)
      //     // // swapStream = context.Response.Body; // Error: Using variable 'swapStream' is immutable
      //     logger.LogDebug($"originalResponseBody = {originalResponseBody}"); // Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpResponseStream
      //     // logger.LogDebug($"originalResponseBody.Length = {originalResponseBody.Length}"); // fails, can't be read
      //     logger.LogDebug($"context.Response.Body = {context.Response.Body}"); // System.IO.MemoryStream // Body is now an empty stream
      //     logger.LogDebug($"context.Response.Body.Length = {context.Response.Body.Length}"); // Length = 0
      //
      //     logger.LogDebug("Calling Invoke next");
      //     await next.Invoke(); // rest of pipeline executes. Output:
      //     // MoviesAPI.Filters.ParseBadRequestFilter[0] - OnActionExecuting called 
      //     // MoviesAPI.Controllers.GenresController[0] - HttpGet getGenre : Return genre with given id
      //     // MoviesAPI.Filters.ParseBadRequestFilter[0] - OnActionExecuted called 
      //     // MoviesAPI.Filters.ParseBadRequestFilter[0] - OnActionExecuted: statusCode = ?
      //     logger.LogDebug("Invoke next finished"); // The rest of the pipeline has completed.
      //
      //     // Now the Body has been replaced with the response body, and the Body and swapStream point to the same content,
      //     // so swapStream now contains the Response Body content
      //     logger.LogDebug($"context.Response.Body = {context.Response.Body}"); // System.IO.MemoryStream
      //     logger.LogDebug($"context.Response.Body.Length = {context.Response.Body.Length}"); // Length = 24
      //     logger.LogDebug($"swapStream.Length = {swapStream.Length}"); // Length = 24
      //
      //     swapStream.Seek(0, SeekOrigin.Begin);
      //     string responseBody = new StreamReader(swapStream).ReadToEnd();
      //     logger.LogDebug($"swapStream read into: responseBody = {responseBody}"); // {"id":2,"name":"Action"}
      //     swapStream.Seek(0, SeekOrigin.Begin); 
      //
      //     logger.LogDebug($"swapStream.Length = {swapStream.Length}"); // Length = 24
      //     logger.LogDebug($"originalResponseBody = {originalResponseBody}"); // Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpResponseStream
      //     logger.LogDebug("Copying originalResponseBody contents to swapStream");
      //     await swapStream.CopyToAsync(originalResponseBody);
      //     logger.LogDebug("originalResponseBody copied to swapStream");
      //     logger.LogDebug($"swapStream.Length = {swapStream.Length}"); // Length = 24
      //     logger.LogDebug($"originalResponseBody = {originalResponseBody}"); // Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpResponseStream
      //     context.Response.Body = originalResponseBody; // Finish by pointing it back to the correct object pointer, not to swapStream, so swapStream can be garbage collected???
      //
      //     logger.LogDebug("Finished swap stream process:");
      //     logger.LogDebug($"originalResponseBody = {originalResponseBody}"); // Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpResponseStream
      //     logger.LogDebug($"responseBody = {responseBody}"); // {"id":2,"name":"Action"}
      //     logger.LogDebug($"context.Response.Body = {context.Response.Body}"); // Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpResponseStream
      //     // logger.LogDebug($"context.Response.Body.Length = {context.Response.Body.Length}"); // Fail, now is original object, which can't be read.
      //     logger.LogDebug($"swapStream.Length = {swapStream.Length}"); // Length = 24
      //   }
      // });

      app.UseHttpsRedirection();
      app.UseRouting();
      // app.UseResponseCaching(); // 113. Filters - remove from final code
      app.UseCors();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
      // app.UseHttpLogging();
    }
  }
}