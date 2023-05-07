mod fetch;
mod input;
mod mode;
mod write;

use anyhow::Result;
use config::Config;
use log::info;
use chrono::Utc;

#[tokio::main]
async fn main() -> Result<()> {
    env_logger::init();

    let settings: input::Input = fetch_settings()?;

    match settings.program_mode
    {
        mode::Mode::ReadWeatherData =>  read_weather_data(&settings).await?
    };

    Ok(())
}

async fn read_weather_data(settings: &input::Input) -> Result<()> {
    info!("Start fetching weather data...");
    let result = fetch::fetch_weather_data(&settings.weather_api_base_url, &settings.latitude, &settings.longitude, &settings.weather_api_key, &Utc::now()).await?;

    info!("Start writing weather data...");
    let map = write::WeatherReading {
        wind_speed: result.wind.speed,
        time: Utc::now(),
    };
    write::write_weather_data(map, &settings.influx_db_base_url, "homeassistant", &settings.influx_db_token).await?;
    Ok(())
}

fn fetch_settings() -> Result<input::Input, config::ConfigError> {
    let settings = Config::builder()
        // Add in `./Settings.toml`
        .add_source(config::File::with_name("./Settings").required(false))
        .add_source(config::Environment::with_prefix("weather"))
        .build()
        .unwrap();

    settings.try_deserialize::<input::Input>()
}
