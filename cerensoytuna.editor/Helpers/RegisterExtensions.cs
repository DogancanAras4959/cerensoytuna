using cerensoytuna.CORE.UnitOfWork;
using cerensoytuna.DAL;
using cerensoytuna.ENGINE.Engines;
using cerensoytuna.ENGINE.Interface;
using cerensoytuna.ENGINES.Engines;
using cerensoytuna.ENGINES.Interface;
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

namespace cerensoytuna.editor.Helpers
{
    internal static class RegisterExtensions
    {
        internal static void AddDbContextDI(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var contextConnectionString = configuration.GetConnectionString("Default");
            services.AddDbContextPool<cerensoytunadbcontext>(x => x.UseSqlServer(contextConnectionString, o =>
            {
                o.EnableRetryOnFailure(3);
            }).EnableSensitiveDataLogging(environment.IsDevelopment())
              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );
        }

        internal static void AddInjections(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IRoleService), typeof(RoleService));
            services.AddTransient(typeof(ICountService), typeof(CountService));
            services.AddTransient(typeof(IPostService), typeof(PostService));
            services.AddTransient(typeof(ISettingService), typeof(SettingService));
            services.AddTransient(typeof(ISeoService), typeof(SeoService));
            services.AddTransient(typeof(ILangService), typeof(LangService));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

    }
}
