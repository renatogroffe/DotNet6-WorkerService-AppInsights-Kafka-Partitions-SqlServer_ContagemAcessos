using WorkerContagem;
using WorkerContagem.Data;
using Microsoft.ApplicationInsights.DependencyCollector;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ContagemRepository>();
        services.AddHostedService<Worker>();

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>(
            (module, o) =>
            {
                module.EnableSqlCommandTextInstrumentation = true;
            });
    })
    .Build();

await host.RunAsync();