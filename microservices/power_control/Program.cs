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
                    Console.WriteLine("Peak too high. Shutting down heat pump.");
                    await client.GetAsync(
                        "http://192.168.1.121/control?cmd=heatpump&set_power_mode=off"
                    );
                    await client.PostAsJsonAsync<DiscordMessage>(
                        "https://discord.com/api/webhooks/1455072237207158981/C9qvSIGMZVc60VwpZizxqKigyzvA182RDSdt9k8qtWTq-fBgzlJHh53wAYIqYkGNkBM3",
                        new DiscordMessage("Peak too high. Shutting down heat pump.", "willemBot")
                    );
                    break;
                case "resolved":
                    Console.WriteLine("Peak acceptable. Re enabling heat pump.");
                    await client.GetAsync(
                        "http://192.168.1.121/control?cmd=heatpump&set_power_mode=on"
                    );
                    await client.PostAsJsonAsync<DiscordMessage>(
                        "https://discord.com/api/webhooks/1455072237207158981/C9qvSIGMZVc60VwpZizxqKigyzvA182RDSdt9k8qtWTq-fBgzlJHh53wAYIqYkGNkBM3",
                        new DiscordMessage("Peak is acceptable. Re enabling heat pump.", "willemBot")
                    );
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

record DiscordMessage(string content, string userName);
