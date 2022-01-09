﻿using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "ce4046dfc5054591b60e568287340fb8";
                o.LogId = new Guid("639010b6-540d-4230-9025-1283ccb335a7");
            });


            //configurando o Logging com o Elmah, neste caso desabilitamos e fizemos via Middlware
            //services.AddLogging(builder =>
            //{
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "ce4046dfc5054591b60e568287340fb8";
            //        o.LogId = new Guid("639010b6-540d-4230-9025-1283ccb335a7");
            //    });
            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});

            //services.AddHealthChecks()
            //    .AddElmahIoPublisher(options =>
            //    {
            //        options.ApiKey = "388dd3a277cb44c4aa128b5c899a3106";
            //        options.LogId = new Guid("c468b2b8-b35d-4f1a-849d-f47b60eef096");
            //        options.HeartbeatId = "API Fornecedores";

            //    })
            //    .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
            //    .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
