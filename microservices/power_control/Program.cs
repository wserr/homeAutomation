using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add JSON serialization services
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

var app = builder.Build();

app.MapPost(
        "/high_power_alert",
        async (AlertBody body) =>
        {
            Console.WriteLine("high power alert was received.");

            using var client = new HttpClient();
            switch (body.Status)
            {
                case "firing":
                    await client.GetAsync(
                        "http://192.168.1.121/control?cmd=heatpump&set_power_mode=off"
                    );
                    Console.WriteLine("Peak too high. Shutting down heat pump.");
                    break;
                case "resolved":
                    await client.GetAsync(
                        "http://192.168.1.121/control?cmd=heatpump&set_power_mode=on"
                    );
                    Console.WriteLine("Peak acceptable. Re enabling heat pump.");
		    break;
                default:
                    Console.WriteLine("Unknown body type");
                    break;
            }
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

record AlertBody(string Status);
