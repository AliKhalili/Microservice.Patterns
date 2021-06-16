using System;
using FlakyApi.Utils;
using FlakyApi.Utils.Strategy;
using FlakyApi.Utils.WeatherApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FlakyApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlakyApi", Version = "v1" });
            });

            //services.Configure<OpenWeatherOption>(options =>
            //{
            //    options.ApiKey = Environment.GetEnvironmentVariable(OpenWeatherOption.EnvironmentApiKey);
            //    options.ApiKey = "960980ac630e86ac4a5729f5a021ce63";
            //});
            services.AddLogging();
            services.AddHttpClient();
            services.AddSingleton<IFlakyStrategy>(_ => new CircuitBreakerFlakyStrategy(TimeSpan.FromSeconds(1)));
            services.AddScoped<FakeWeatherService>();
            services.AddScoped<IWeatherService, FlakyWeatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlakyApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
