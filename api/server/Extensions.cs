﻿using FSH.WebApi.Modules.Catalog;
using JasperFx.CodeGeneration;
using Wolverine;

namespace FSH.WebApi.Server;

public static class Extensions
{
    public static WebApplicationBuilder AddModules(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        //register wolverine with module assemblies
        builder.Host.UseWolverine(options =>
        {
            options.Discovery.IncludeAssembly(typeof(CatalogModule).Assembly);
            options.CodeGeneration.TypeLoadMode = TypeLoadMode.Auto;
        });
        return builder;
    }

    public static WebApplication UseModules(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        //register modules
        app.UseCatalogModule();

        //register endpoints
        var endpoints = app.NewVersionedApi().MapGroup("api").HasApiVersion(1.0);
        endpoints.MapCatalogEndpoints();

        return app;
    }
}