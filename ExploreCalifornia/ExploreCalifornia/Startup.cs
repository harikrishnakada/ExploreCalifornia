using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ExploreCalifornia
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run() is a middle ware. You can use the multiple middllw wares. When user makes a request it will only exceute in the order that you register the middleware in the pipeline.
            //Shift the focus to more effective way to register the request handeling logic the "app.Use()".
            //Like wise app.Run(), app.Use() also holds the function to handle the request processing logic.
            //app.Use() has two parameters, first one represents the request 'context' and the second parameter 'next' represents the next middleware function registered in the pipeline.
            //Use next() to execute the next middle ware.
            app.Use(async (context, next) =>
            {
                //Checks the url with the pattern '/hello'
                if (context.Request.Path.Value.StartsWith("/hello"))
                {
                    await context.Response.WriteAsync("Hello World!");
                }
                await next();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
