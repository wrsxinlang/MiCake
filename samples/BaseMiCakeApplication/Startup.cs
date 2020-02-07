using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiCake.EntityFrameworkCore;
using BaseMiCakeApplication.EFCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using MiCake.AspNetCore.Extension;

namespace BaseMiCakeApplication
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
            services.AddControllers(options =>
            {

            });

            services.AddDbContext<BaseAppDbContext>(options =>
            {
                options.UseMySql("Server=localhost;Database=micakeexample;User=root;Password=a12345;", mySqlOptions => mySqlOptions
                    .ServerVersion(new ServerVersion(new Version(10, 5, 0), ServerType.MariaDb)));
            });

            services.AddMiCake<BaseMiCakeModule>(builer =>
            {
                builer.UseAspNetCore(mvcOptions =>
                {
                });
                builer.UseEFCore<BaseAppDbContext>();
            });

            //Add Swagger
            services.AddSwaggerDocument(document => document.DocumentName = "MiCake Demo Application");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.InitMiCake();

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}