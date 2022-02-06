using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SpaceFlightNews.Data;
using SpaceFlightNews.DTO;
using SpaceFlightNews.Interfaces.Repository;
using SpaceFlightNews.Jobs;
using SpaceFlightNews.Repository.Repository;
using SpaceFlightNews.Services;
using SpaceFlightNews.Singleton;

namespace SpaceFlightNews
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ConnectionString")));

            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddScoped<IArticleRepository, ArticleRepository>();


            services.AddSingleton<UpdateDatabaseJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(UpdateDatabaseJob),
                cronExpression: "0 0 9 * * ?"));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "spaceflight", Version = "v1" });
        });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpaceFlighNews API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
