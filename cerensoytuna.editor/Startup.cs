using AutoMapper;
using cerensoytuna.editor.Helpers;
using cerensoytuna.editor.Profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.editor
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

            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddDistributedMemoryCache();
            services.AddDbContextDI(_configuration, Environment);
            services.AddInjections();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PostProfile());
                mc.AddProfile(new SeoProfile());
                mc.AddProfile(new SiteProfile());
                mc.AddProfile(new UserProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(
            CookieAuthenticationDefaults.AuthenticationScheme, (options) =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/hesap/girisyap";
                options.LogoutPath = "/hesap/cikisyap";
                options.AccessDeniedPath = "/hesap/yetkisizgiris";
                options.SlidingExpiration = true;
                options.Events = new CookieAuthenticationEvents
                {
                    OnSigningIn = async context =>
                    {
                        await Task.CompletedTask;
                    },
                    OnSignedIn = async context =>
                    {
                        await Task.CompletedTask;
                    },
                    OnValidatePrincipal = async context =>
                    {
                        await Task.CompletedTask;
                    }

                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/yonetim/hata");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=yonetim}/{action=anasayfa}/{id?}");
            });
        }
    }
}
