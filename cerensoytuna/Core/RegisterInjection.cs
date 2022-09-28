using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cerensoytuna.DAL;
using cerensoytuna.ENGINES.Interface;
using cerensoytuna.ENGINES.Engines;
using cerensoytuna.CORE.UnitOfWork;

namespace cerensoytuna.Core
{
    internal static class RegisterInjection
    {
        internal static void AddDbContextDI(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var contextConnectionString = configuration.GetConnectionString("Default");
            services.AddDbContextPool<cerensoytunadbcontext>(x => x.UseSqlServer(contextConnectionString, o =>
            {
                o.EnableRetryOnFailure(3);
            })
                .EnableSensitiveDataLogging(environment.IsDevelopment())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }

        internal static void AddInjections(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IPostService), typeof(PostService));
            services.AddTransient(typeof(ISettingService), typeof(SettingService));
            services.AddTransient(typeof(IEmailSender), typeof(EmailSender));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

    }

}
