using Contracts;
using Infrastucture;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.Infrastructure;
using AutoMapper;
using Api.Automapper;

namespace Api
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
            services.AddOpenApiDocument(config =>
            {
                config.AllowReferencesWithProperties = true;
                config.Title = "Credit Calculator Documentation";
            });

            services.AddAutoMapper(typeof(MapperProfile));

            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq(MassTransitBusFactory.ConfigureBus);
                cfg.AddRequestClient<UtilizeCreditRequested>();
                cfg.AddRequestClient<CalculateCreditRequested>();
            });

            services.AddMassTransitHostedService();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3(x =>
            {
                x.DocumentTitle = "Credit Calculator";
            });

            app.UseRouting();

            app.UseCors(Configuration);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
