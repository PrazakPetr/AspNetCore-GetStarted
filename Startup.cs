using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using GetStarted.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using GetStarted.Entities;

namespace GetStarted
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(@"C:\MyTutorials\AspNetCore\GetStarted\appsettings.json");

            this.Configuration = builder.Build();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<GetStartedDbContext>(options => options.UseSqlServer(Configuration["database:connection"]));

            services.AddSingleton<IConfiguration>(this.Configuration);
            services.AddSingleton<IGreetingService, JsonGreetingService>();
#pragma warning disable S125 // Sections of code should not be "commented out"
                            //services.AddScoped<IRestarantRepository, InMemoryRestaurantRepository>();
            services.AddScoped<IRestarantRepository, DbRestaurantRepsoitory>();
#pragma warning restore S125 // Sections of code should not be "commented out"
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IGreetingService greetingService)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();
            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.UseMvc(ConfigureRoutes);

            app.Run(async (context) =>
            {
                var greeting = greetingService.GetGreeting();
                await context.Response.WriteAsync(greeting);
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action}/{id?}",
                new { action = "Index" }
                );

        }
    }
}
