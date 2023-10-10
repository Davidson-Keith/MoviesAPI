using Microsoft.OpenApi.Models;
using MoviesAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MoviesAPI.Filters;

namespace MoviesAPI {
  public class Startup {
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }
    
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.AddControllers(options => {
        options.Filters.Add(typeof(MyExceptionFilter));
      });
      services.AddResponseCaching();
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
      services.AddTransient<MyActionFilter>();
      
      // AddSingleton = singleton.
      // AddTransient = new object every request.
      // AddScoped = new object per http request (not per session, but maybe there is an option for that too).
      services.AddSingleton<IRepository, InMemoryRepository>();
      
      // services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MoviesAPI", Version = "v1" }); });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) {
      if (env.IsDevelopment()) {
        // app.UseDeveloperExceptionPage();
        // app.UseSwagger();
        // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoviesAPI v1"));
      }
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseResponseCaching();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}