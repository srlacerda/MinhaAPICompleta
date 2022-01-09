using AutoMapper;
using DevIO.Api.Configuration;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevIO.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public Startup(IHostEnvironment hostEnvironment)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(hostEnvironment.ContentRootPath)
        //        .AddJsonFile("appsettings.json", true, true)
        //        .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
        //        .AddEnvironmentVariables();

        //    if (hostEnvironment.IsDevelopment())
        //    {
        //        builder.AddUserSecrets<Startup>();
        //    }

        //    Configuration = builder.Build();
        //}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MeuDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityConfiguration(Configuration);

            services.AddAutoMapper(typeof(Startup));

            services.AddApiConfig();

            services.AddSwaggerConfig();

            services.AddLoggingConfig(Configuration);

            //comentar para rodar o migrations
            services.ResolveDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseApiConfig(env);

            app.UseSwaggerConfig(provider);

            app.UseLoggingConfiguration();

            //if (env.IsDevelopment())
            //{
            //    app.UseCors("Development");
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseCors("Production");
            //    app.UseHsts();
            //}

            //app.UseAuthentication(); //precisa SEMPRE vir antes do app.UseMvcConfiguration(env);
            //app.UseMvcConfiguration(env);
        }
    }
}
