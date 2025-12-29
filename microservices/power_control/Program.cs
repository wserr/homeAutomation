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
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(body));

	    using var client = new HttpClient();
	    await client.GetAsync("http://192.168.1.121/control?cmd=heatpump&set_power_mode=off");
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
