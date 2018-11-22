using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using WebClient.Core;
using WebClient.Extensions;
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
            WebConfig.ConnectionString = this.Configuration.GetSection("ConnectionString").Value;
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

            var assembly = Assembly.GetExecutingAssembly();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                // Redirect to path when unathorized. For example "/login"
                options.LoginPath = "/login";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PermissionRequirement.PermissionPolicies.Permission, policy => policy.Requirements.Add(new PermissionRequirement()));
                options.AddPolicy(PermissionRequirement.PermissionPolicies.PermissionUri, policy => policy.Requirements.Add(new PermissionRequirement { IsModelUri = true }));
            });
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            // Add session
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddHttpContextAccessor();

            var builder = new ContainerBuilder();

            builder.Populate(services);

            // Resgister Services
            builder.RegisterType<AccountService>().As<IAccountService>();

            // Register Repositories
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<AuthHelper>().SingleInstance();

            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Hostring environment</param>
        /// <param name="loggerFactory">Logger factory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            env.ConfigureNLog("nlog.config");
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/error/{0}");

                // app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
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
