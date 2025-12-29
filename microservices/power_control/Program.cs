using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet(
        "/high_power_alert",
        () =>
        {
            Console.WriteLine("high power alert was received.");
            return new OkResult();
        }
    )
    .WithName("HighPowerAlert");

app.MapGet(
        "/low_power_alert",
        () =>
        {
            Console.WriteLine("low power alert was received.");
            return new OkResult();
        }
    )
    .WithName("Low Power Alert");

app.Run();
