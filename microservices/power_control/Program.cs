using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapPost(
        "/high_power_alert",
        (object body) =>
        {
            Console.WriteLine("high power alert was received.");
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(body));
            return new OkResult();
        }
    )
    .WithName("HighPowerAlert");

app.MapPost(
        "/low_power_alert",
        () =>
        {
            Console.WriteLine("low power alert was received.");
            return new OkResult();
        }
    )
    .WithName("Low Power Alert");

app.Run();
