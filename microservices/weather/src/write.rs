use chrono::{DateTime, Utc};
use influxdb::{Client, InfluxDbWriteable};
use anyhow::Result;

#[derive(InfluxDbWriteable)]
pub struct WeatherReading {
    pub time: DateTime<Utc>,
    pub wind_speed: f32,
}

pub async fn write_weather_data(
    reading: WeatherReading,
    base_url: &str,
    database: &str,
    token: &str,
) -> Result<()> {
    let client = Client::new(base_url, database).with_token(token);
    let query = reading.into_query("WeatherReading");
    client.query(query).await?;
    Ok(())
}
