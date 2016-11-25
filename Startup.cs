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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GetStarted.Middleware;

namespace GetStarted
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {


            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(@".\appsettings.json");

            this.Configuration = builder.Build();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<GetStartedDbContext>(options => options.UseSqlServer(Configuration["mainDatabase:connection"]));

            services.AddEntityFrameworkSqlite()
                .AddDbContext<GetStartedIdentityDbConxtext>(options => options.UseSqlite(Configuration["identityDatabase:connection"]));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<GetStartedIdentityDbConxtext>();

            services.AddSingleton<IConfiguration>(this.Configuration);
            services.AddSingleton<IGreetingService, JsonGreetingService>();

           // services.AddScoped<IRestarantRepository, InMemoryRestaurantRepository>();
            services.AddScoped<IRestarantRepository, DbRestaurantRepsoitory>();
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

            app.UseNodeModules(env);
            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.UseIdentity();

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
