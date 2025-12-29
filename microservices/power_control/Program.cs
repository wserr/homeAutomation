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
	    var heatPumpState = await client.GetFromJsonAsync<HeatPumpStatus>("http://192.168.1.121/control");
	    var message = "";
	    Console.WriteLine(heatPumpState.power);
            switch (body.Status)
            {
                case "firing":
                    Console.WriteLine("Peak too high. Shutting down heat pump.");
		    message = "@everyone peak is too high. Disabling heat pump. 🥶";
		    if (heatPumpState.power == "on")
		    {
                    	await client.GetAsync(
                    	    "http://192.168.1.121/control?cmd=heatpump&set_power_mode=off"
                    	);
		    }
		    else
		    {
			    message = "@everyone high power alert, but heat pump could not be disabled because state was not 'ON'";
		    }
                    await client.PostAsJsonAsync<DiscordMessage>(
                        "https://discord.com/api/webhooks/1455072237207158981/C9qvSIGMZVc60VwpZizxqKigyzvA182RDSdt9k8qtWTq-fBgzlJHh53wAYIqYkGNkBM3",
                        new DiscordMessage(message, "willemBot")
                    );
                    break;
                case "resolved":
                    Console.WriteLine("Peak acceptable. Re enabling heat pump. 🌶️");
		    message = "Re enabled heat pump.";
		    if (heatPumpState.power == "off")
		    {
                    	await client.GetAsync(
                    	    "http://192.168.1.121/control?cmd=heatpump&set_power_mode=on"
                    	);
		    }
		    else {
			    message = "@everyone Heat pump could not be re enabled because state was not 'ON'";
		    }
                    await client.PostAsJsonAsync<DiscordMessage>(
                        "https://discord.com/api/webhooks/1455072237207158981/C9qvSIGMZVc60VwpZizxqKigyzvA182RDSdt9k8qtWTq-fBgzlJHh53wAYIqYkGNkBM3",
                        new DiscordMessage(message, "willemBot")
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

record HeatPumpStatus(string power);
