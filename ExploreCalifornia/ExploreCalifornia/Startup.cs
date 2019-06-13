using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly IConfigurationRoot configuration;
        public Startup(IHostingEnvironment env)
        {
            configuration = new ConfigurationBuilder()
                                      .AddEnvironmentVariables()
                                      .AddJsonFile(env.ContentRootPath + "/config.json")
                                      .Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //IServiceCollection exposes the hand full of methods that allows to configure the DI
            //Some of the Important methods are 
            //AddScoped: Will only create one instance of that type of each web request. 
            //AddSingleTop :  Create only one instance for the entire application.
            //AddTranisient: Shortest lifespan- An instance will b created every time one is requested. 
            //Each of these methods allows you tell ASP NET core how to create instances of types when they are requested.
            //The primary difference betwween them is how long that instance lives.

            services.AddTransient<FeatureToogles>(x => new FeatureToogles {
                EnableDeveloperExceptions = configuration.GetValue<bool>("enableDeveloperException")
            });

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        //Dependency injection is a development pattern which encourages loose coupling between components.
        //This FeatureToogles instance will be injected by asp .net core dependecy injection framework
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FeatureToogles features)
        {
            //use the cofigiration API to configure the specific features.
            #region configuration
            //Configuration API using the environment variables.
            //The configurations can be given in the launchSettings.json or through the solution properties under the environment variables.

            //var configuration = new ConfigurationBuilder()
            //                            .AddEnvironmentVariables()
            //                            .Build();

            //You can also read the configurations from the user defined json 
          
            #endregion

            if (features.EnableDeveloperExceptions)
            {
                //This is used to display the default Exception page provided by the Microsoft. In order to over ride this and display user defined exeption page,
                //we may need to add out custom error message static html file to to the wwwroot folder.
                // And also currently this project is useing the default development environment. We may need to change it to the custom environment say 'Product'.
                // You can change it in the solution properties environment variables.
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Define the custion exception page.
                app.UseExceptionHandler("/error.html");
            }

            #region Non Static content
            //app.Run() is a middle ware. You can use the multiple middllw wares. When user makes a request it will only exceute in the order that you register the middleware in the pipeline.
            //Shift the focus to more effective way to register the request handeling logic the "app.Use()".
            //Like wise app.Run(), app.Use() also holds the function to handle the request processing logic.
            //app.Use() has two parameters, first one represents the request 'context' and the second parameter 'next' represents the next middleware function registered in the pipeline.
            //Use next() to execute the next middle ware.

            //comment this below, if you want to run static html files

            //app.Use(async (context, next) =>
            //{
            //    //Checks the url with the pattern '/hello'
            //    if (context.Request.Path.Value.StartsWith("/hello"))
            //    {
            //        await context.Response.WriteAsync("Hello ASP NET Core!");
            //    }
            //    await next();
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            #endregion

            #region Error Hadling
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR !");

                await next();

            });

            #endregion

            #region Static content
            //This app.UseFileServer() call will register the static file middle ware component which tells ASPNET Core to try to match
            //any unhadeld requests to a file in the 'wwwroot' folder and if that file exixts it will serve that file.
            //This Mapping includes the defalut files (index.html)
            //Use the static files middleware to render the any kind of static file content.

            app.UseFileServer();

            #endregion
            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
