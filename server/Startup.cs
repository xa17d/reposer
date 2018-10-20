using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using reposer.Config;
using reposer.Repository;
using reposer.Rendering;
using reposer.Rendering.CopyHtml;

namespace reposer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ConfigService>(ConfigService.Instance);

            var pollerType = ConfigService.Instance.WebsiteRepositoryPollerType;
            switch (pollerType)
            {
                case "git":
                    services.AddSingleton<IRepositoryPullService, GitPullService>();
                    break;

                case "debug":
                    services.AddSingleton<IRepositoryPullService, DebugPullService>();
                    break;

                default: throw new ArgumentException($"Unknown {nameof(pollerType)}: {pollerType}");
            }

            var rendererType = ConfigService.Instance.RenderType;
            switch (rendererType)
            {
                case "copy-html":
                    services.AddTransient<IRendererFactory, CopyHtmlRendererFactory>();
                    break;

                default: throw new ArgumentException($"Unknown {nameof(rendererType)}: {rendererType}");
            }
            services.AddSingleton<RenderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles(); // important that this is before UseStaticFiles

            var cachePeriod = env.IsDevelopment() ? "0" : "86400";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });

            //app.UseStatusCodePages();

            app.ApplicationServices.GetService<RenderService>(); // Start Render Service
        }
    }
}
