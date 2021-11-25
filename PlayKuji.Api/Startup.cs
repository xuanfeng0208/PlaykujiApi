using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayKuji.Api.Filters;
using PlayKuji.BusinessLayer.Logics;
using PlayKuji.DataLayer.Managers;
using PlayKuji.DataLayer.Repositorys;
using PlayKuji.Domain.Entities;
using PlayKuji.Domain.Interfaces.Logics;
using PlayKuji.Domain.Interfaces.Managers;
using PlayKuji.Domain.Interfaces.Repositories;
using PlayKuji.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayKuji.Api
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
            services.AddDbContext<KujiContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddHttpClient<HttpClientHelper>();

            services.AddScoped(typeof(DbContext), provider => provider.GetService<KujiContext>());
            services.AddScoped<HttpClientHelper>();
            services.AddScoped<IProductLogic, ProductLogic>();
            services.AddScoped<IProductRepository, ProcuctRepository>();
            services.AddScoped<IProductAwardRepository, ProductAwardRepository>();
            services.AddScoped<ICrawlerManager, CrawlerManager>();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(AuthorizationFilter));
                options.Filters.Add(typeof(ExceptionFilter));
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
