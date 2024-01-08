using MqttDashboard.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "MQTT Collector Service";
    })
    .ConfigureServices((context, services) => {})
    .ConfigureHostConfiguration(conf => {})
    .ConfigureLogging((context, logging) => {})
    .Build();

await host.RunAsync();