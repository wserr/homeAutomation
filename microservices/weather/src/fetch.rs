use chrono::{DateTime, Utc};
use serde::{Deserialize, Serialize};

#[derive(Debug, Serialize, Deserialize)]
pub struct WeatherResponse {
    pub id: usize,
    pub weather: Vec<Weather>,
    pub wind: Wind,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct Weather {
    pub id: usize,
    pub main: String,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct Wind {
    pub speed: f32,
}

fn construct_weather_data_url(
    base_url: &str,
    latitude: &str,
    longitude: &str,
    api_key: &str,
    current_datetime: &DateTime<Utc>,
) -> String {
    format!(
        "{}?lat={}&lon={}&dt={}&appid={}",
        base_url,
        latitude,
        longitude,
        current_datetime.timestamp(),
        api_key
    )
}

pub async fn fetch_weather_data(
    base_url: &str,
    latitude: &str,
    longitude: &str,
    api_key: &str,
    current_datetime: &DateTime<Utc>,
) -> Result<WeatherResponse, reqwest::Error> {
    let url = construct_weather_data_url(base_url, latitude, longitude, api_key, current_datetime);
    reqwest::get(url).await?.json().await
}

#[cfg(test)]
mod test {
    use super::*;
    use chrono::prelude::*;

    #[test]
    fn should_construct_url() {
        let result = construct_weather_data_url(
            "http://test",
            "1.14",
            "1.12",
            "abc",
            &Utc.with_ymd_and_hms(2023, 1, 2, 1, 0, 0).unwrap(),
        );
        assert_eq!(
            "http://test?lat=1.14&lon=1.12&dt=1672621200&appid=abc",
            result
        );
    }
}
