using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VueCliMiddleware;

namespace VeeChee
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddControllers();
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp";
                });
                services.AddSingleton<IConfiguration>(Configuration);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseRouting();
                app.UseSpaStaticFiles();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

                app.UseSpa(spa =>
                {
                    if (env.IsDevelopment())
                        spa.Options.SourcePath = "ClientApp/";// Path.Join(env.ContentRootPath, "ClientApp");
                    else
                        spa.Options.SourcePath = "dist";

                    if (env.IsDevelopment())
                    {
                        spa.UseVueCli(npmScript: "serve");
                        //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    }

                });
            }
            catch(Exception e)
            {
                string message = e.Message;
            }
        }
    }
}
