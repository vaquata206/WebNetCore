using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebClient.Core;
using WebClient.Repositories.Implements;
using WebClient.Repositories.Interfaces;
using WebClient.Services.Implements;
using WebClient.Services.Interfaces;

namespace WebClient
{
    /// <summary>
    /// Start up
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The IHosting Evironment</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(string.Format("appsettings.{0}.json", env.EnvironmentName), optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();

            WebConfig.ApiSystemUrl = this.Configuration.GetSection("ApiSystem").Value;
        }

        /// <summary>
        /// Application container
        /// </summary>
        public IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// The configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; private set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Service provider</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                // Redirect to path when unathorized. For example "/login"
                options.LoginPath = "/login";
                options.Events = new CookieAuthenticationEvents
                {
                    OnSignedIn = context =>
                    {
                        var a = context.HttpContext.Request.Method;
                        return Task.CompletedTask;
                    }
                };
            });

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            var builder = new ContainerBuilder();

            builder.Populate(services);
            // Resgister Services
            builder.RegisterType<AccountService>().As<IAccountService>();

            // Register Repositories
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();

            builder.Register(c => logger).As<NLog.ILogger>().SingleInstance();
            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Hostring environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
