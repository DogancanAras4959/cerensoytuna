using AutoMapper;
using cerensoytuna.Core;
using cerensoytuna.CORE.EmailConfig;
using cerensoytuna.Profiles;
using cerensoytuna.Resource;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cerensoytuna
{
    public class Startup
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            Environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorPagesOptions(options => { options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()); });

            services.AddDbContextDI(_configuration, Environment);
            services.AddInjections();

            services.Configure<EmailConfiguration>(_configuration.GetSection("EmailConfiguration"));
            services.AddDistributedMemoryCache();

            #region Language Options

            services.AddLocalization(options => options.ResourcesPath = "Resource");
            services.AddSingleton<LocalizationService>();

            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(ApplicationResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("ApplicationResource", assemblyName.Name);
                    };
                });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("tr")
                };
                options.DefaultRequestCulture = new RequestCulture("tr");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider()
                {
                    Options = options
                });
            });

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("lang", typeof(LocalizationService));
            });

            #endregion

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PostProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseStatusCodePagesWithReExecute("/anasayfa/hata/{0}");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            var requestLocalizationOptions = app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(requestLocalizationOptions.Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=anasayfa}/{action=sayfa}/{id?}");

                endpoints.MapControllerRoute(
                name: "culture",
                pattern: "{culture}/{controller=anasayfa}/{action=sayfa}/{id?}");

                endpoints.MapControllerRoute(
                name: "islem",
                pattern: "/islem/{Id}/{Title}", new { controller = "anasayfa", action = "islem" });

                endpoints.MapControllerRoute(
                name: "treatment",
                pattern: "/treatment/{Id}/{Title}", new { controller = "home", action = "treatment" });

            });
        }
    }
}
