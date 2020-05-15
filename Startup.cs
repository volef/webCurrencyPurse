using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using webCurrencyPurse.Data;
using webCurrencyPurse.Services;

namespace webCurrencyPurse
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        private readonly IWebHostEnvironment _env;
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
                services.AddDbContext<ApplicationDbContext>(builder =>
                    builder.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            else
                services.AddDbContext<ApplicationDbContext>(builder =>
                    builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddUserManager();
            services.AddCurrencyParser(Configuration["Parser"]);
            services.AddCurrencyConverter();
            services.AddBillManager();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API валютного кошелька",
                    Description = "Web API для тестового задания",
                    Contact = new OpenApiContact
                    {
                        Name = "Кривошеев Дмитрий",
                        Email = "aspcoreback@yandex.ru"
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API валютного кошелька");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}