﻿using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using MeuSiteMVC.Data;
using Microsoft.AspNetCore.Identity;

namespace MeuSiteMVC.Configuration
{
    public static class LoggingConfig
    {
        public static WebApplicationBuilder AddElmahConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ElmahIoOptions>(builder.Configuration.GetSection("ElmahIo"));
            builder.Services.AddElmahIo();

            builder.Logging.Services.Configure<ElmahIoProviderOptions>(builder.Configuration.GetSection("ElmahIo"));
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.AddElmahIo();

            builder.Logging.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);

            return builder;
        }
    }
}
